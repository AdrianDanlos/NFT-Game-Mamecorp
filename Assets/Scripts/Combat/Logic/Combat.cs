using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

public class Combat : MonoBehaviour
{
    // Data Objects
    public static Fighter player;
    public Fighter bot;
    public int botElo;

    // GameObjects related data
    public GameObject playerGameObject;
    public GameObject playerWrapper;
    public GameObject botGameObject;
    public Canvas results;

    // Script references
    public static Movement movementScript;
    Attack attacktScript;
    public static FightersUIData fightersUIDataScript;

    // Positions data
    static Vector3 PlayerStartingPosition = new Vector3(-6, -0.7f, 0);
    static Vector3 BotStartingPosition = new Vector3(6, -0.7f, 0);
    float DistanceAwayFromEachotherOnAttack = 1.25f;

    // Game status data
    public static bool isGameOver = false;
    List<Fighter> fightersOrderOfAttack = new List<Fighter> { };

    private void Awake()
    {
        // From the current gameobject (this) access the movement component which is a script.
        movementScript = this.GetComponent<Movement>();
        attacktScript = this.GetComponent<Attack>();
        fightersUIDataScript = this.GetComponent<FightersUIData>();
    }
    void Start()
    {
        FindGameObjects();
        SetVisibilityOfGameObjects();
        GetFighterScriptComponent();
        GenerateBotData();
        SetFighterPositions();
        SetOrderOfAttacks();
        fightersUIDataScript.SetFightersUIInfo(player, bot, botElo);
        FighterSkin.SetFightersSkin(player, bot);

        //FIXME: ANIMATION TEST
        FighterAnimations.ChangeAnimation(player, FighterAnimations.AnimationNames.IDLE);

        StartCoroutine(InitiateCombat());
    }

    private void GetFighterScriptComponent()
    {
        player = playerGameObject.GetComponent<Fighter>();
        bot = botGameObject.GetComponent<Fighter>();
    }

    private void SetVisibilityOfGameObjects()
    {
        playerGameObject.SetActive(true);
    }
    private void FindGameObjects()
    {
        playerWrapper = GameObject.Find("FighterWrapper");
        playerGameObject = playerWrapper.transform.Find("Fighter").gameObject;
        botGameObject = GameObject.Find("Bot");
        results = GameObject.FindGameObjectWithTag("Results").GetComponent<Canvas>();
    }

    private void SetFighterPositions()
    {
        //Set GameObjects
        playerGameObject.transform.position = PlayerStartingPosition;
        botGameObject.transform.position = BotStartingPosition;

        //Set Objects
        Vector3 playerDestinationPosition = BotStartingPosition;
        Vector3 botDestinationPosition = PlayerStartingPosition;

        player.initialPosition = PlayerStartingPosition;
        playerDestinationPosition.x -= DistanceAwayFromEachotherOnAttack;
        player.destinationPosition = playerDestinationPosition;

        bot.initialPosition = BotStartingPosition;
        botDestinationPosition.x += DistanceAwayFromEachotherOnAttack;
        bot.destinationPosition = botDestinationPosition;
    }

    IEnumerator InitiateCombat()
    {
        Fighter firstAttacker = fightersOrderOfAttack[0];
        Fighter secondAttacker = fightersOrderOfAttack[1];

        while (!isGameOver)
        {
            // The CombatLogicHandler method should handle all the actions of a player for that turn. E.G. Move, Attack, Throw skill....
            yield return StartCoroutine(CombatLogicHandler(firstAttacker, secondAttacker));
            if (isGameOver) break;
            yield return StartCoroutine(CombatLogicHandler(secondAttacker, firstAttacker));
        }

        //TODO: Send the correct value for the boolean here
        PostGameActions.UpdateElo(User.Instance.elo, botElo, true);
    }

    private void GenerateBotData()
    {
        string botName = MatchMaking.FetchBotRandomName();
        botElo = MatchMaking.GenerateBotElo(User.Instance.elo);

        // Get all the existing cards and add them to the list of cards of the fighter
        List<OrderedDictionary> cardCollection = CardCollection.cards;
        List<Card> botCards = new List<Card>();

        foreach (OrderedDictionary card in cardCollection)
        {
            Card cardInstance = new Card((string)card["cardName"], (int)card["mana"], (string)card["text"], (string)card["rarity"], (string)card["type"]);
            botCards.Add(cardInstance);
        }
        bot.FighterConstructor(botName, 10, 1, 6, "Leaf", "MonsterV5", 1, 0, 10, botCards);
    }

    IEnumerator CombatLogicHandler(Fighter attacker, Fighter defender)
    {
        // Move forward
        yield return StartCoroutine(movementScript.MoveForward(attacker, attacker.destinationPosition));

        // Attack

        int attackCounter = 0;

        while (!isGameOver && (attackCounter == 0 || attacktScript.IsAttackRepeated(attacker)))
        {
            yield return StartCoroutine(attacktScript.PerformAttack(attacker, defender));
            attackCounter++;
        };

        // Move back
        FighterSkin.SwitchFighterOrientation(attacker.GetComponent<SpriteRenderer>());
        yield return StartCoroutine(movementScript.MoveBack(attacker, attacker.initialPosition));
        FighterSkin.SwitchFighterOrientation(attacker.GetComponent<SpriteRenderer>());

        if (isGameOver)
        {
            results.enabled = true;
        }
    }

    // This method creates a dictionary with the Fighter class objects sorted by their speeds to get the order of attack.
    // Higher speeds will get sorted first
    private void SetOrderOfAttacks()
    {
        OrderedDictionary fighterDictWithSpeed = new OrderedDictionary
        {
            {player, player.speed},
            {bot, bot.speed},
        };

        var fighterDictSortedBySpeed = fighterDictWithSpeed.Cast<DictionaryEntry>()
                       .OrderByDescending(r => r.Value)
                       .ToDictionary(c => c.Key, d => d.Value);

        foreach (var fighter in fighterDictSortedBySpeed)
        {
            fightersOrderOfAttack.Add((Fighter)fighter.Key);
        }
    }
}
