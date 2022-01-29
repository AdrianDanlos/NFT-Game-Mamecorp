using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;

public class Combat : MonoBehaviour
{
    // Data Objects
    public Fighter player;
    public Fighter bot;
    int botElo;

    // GameObjects related data
    public GameObject playerGameObject;
    public GameObject playerWrapperGameObject;
    public GameObject botGameObject;

    // Script references
    public static Movement movementScript;
    Attack attacktScript;

    // Positions data
    static Vector3 PLAYER_STARTING_POSITION = new Vector3(-7, 2, 0);
    static Vector3 BOT_STARTING_POSITION = new Vector3(7, 2, 0);
    float DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK = 1;
    Vector3 playerDestinationPosition = BOT_STARTING_POSITION;
    Vector3 botDestinationPosition = PLAYER_STARTING_POSITION;


    // Game status data
    public static bool isGameOver = false;
    List<Fighter> fightersOrderOfAttack = new List<Fighter> { };

    private void Awake()
    {
        // From the current gameobject (this) access the movement component which is a script.
        movementScript = this.GetComponent<Movement>();
        attacktScript = this.GetComponent<Attack>();
    }
    void Start()
    {
        FindFighterGameObjects();
        EnablePlayerGameObject();
        SetPlayerGameObjectInsideCanvas();
        GetFighterScriptComponent();
        GenerateBotData();
        SetFighterPositions();
        SetOrderOfAttacks();
        StartCoroutine(InitiateCombat());
    }

    private void GetFighterScriptComponent()
    {
        player = playerGameObject.GetComponent<Fighter>();
        bot = botGameObject.GetComponent<Fighter>();
    }

    private void SetPlayerGameObjectInsideCanvas()
    {
        playerWrapperGameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        playerWrapperGameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void EnablePlayerGameObject()
    {
        playerGameObject.SetActive(true);
    }

    //TODO: Reuse this in the future whenever we get the player game object in other scenes
    private void FindFighterGameObjects()
    {
        playerWrapperGameObject = GameObject.Find("FighterWrapper");
        playerGameObject = playerWrapperGameObject.transform.Find("Fighter").gameObject;
        botGameObject = GameObject.Find("Bot");
    }

    private void SetFighterPositions()
    {
        player.initialPosition = PLAYER_STARTING_POSITION;
        playerDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
        player.destinationPosition = playerDestinationPosition;

        bot.initialPosition = BOT_STARTING_POSITION;
        botDestinationPosition.x += DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
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
        bot.FighterConstructor(botName, 10, 1, 6, "Leaf", 1, 10, botCards);
    }

    IEnumerator CombatLogicHandler(Fighter attacker, Fighter defender)
    {
        // Move forward
        yield return StartCoroutine(movementScript.MoveForward(attacker, attacker.destinationPosition));

        // Attack

        int attackCounter = 0;

        while (!isGameOver && (attackCounter == 0 || attacktScript.IsAttackRepeated(attacker)))
        {
            yield return StartCoroutine(attacktScript.PerformAttack(attacker, defender, player));
            attackCounter++;
        };

        // Move back
        yield return StartCoroutine(movementScript.MoveBack(attacker, attacker.initialPosition));
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
