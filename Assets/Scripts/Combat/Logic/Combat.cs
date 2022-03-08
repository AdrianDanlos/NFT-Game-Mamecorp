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

    // GameObjects data
    public static GameObject playerGameObject;
    public GameObject playerWrapper;
    public static GameObject botGameObject;
    public Canvas results;

    // Script references
    public static Movement movementScript;
    Attack attacktScript;
    public static FightersUIData fightersUIDataScript;

    // Positions data
    static Vector3 PlayerStartingPosition = new Vector3(-6, -0.7f, 0);
    static Vector3 BotStartingPosition = new Vector3(6, -0.7f, 0);
    float DistanceAwayFromEachotherOnAttack = 1.5f;

    // Game status data
    public static bool isGameOver;
    List<Fighter> fightersOrderOfAttack = new List<Fighter> { };
    private float playerMaxHp;

    private void Awake()
    {
        // From the current gameobject (this) access the movement component which is a script.
        movementScript = this.GetComponent<Movement>();
        attacktScript = this.GetComponent<Attack>();
        fightersUIDataScript = this.GetComponent<FightersUIData>();
        isGameOver = false;
    }

    void Start()
    {
        FindGameObjects();
        SetVisibilityOfGameObjects();
        GetFighterScriptComponents();
        ResetAnimationsState();
        GenerateBotData();
        SetFighterPositions();
        SetOrderOfAttacks();
        fightersUIDataScript.SetFightersUIInfo(player, bot, botElo);
        FighterSkin.SetFightersSkin(player, bot);
        playerMaxHp = player.hp;

        StartCoroutine(InitiateCombat());
    }

    private void GetFighterScriptComponents()
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

        //1 loop = 1 turn (both players attacking)
        while (!isGameOver)
        {
            // The CombatLogicHandler method should handle all the actions of a player for that turn. E.G. Move, Attack, Throw skill....
            yield return StartCoroutine(CombatLogicHandler(firstAttacker, secondAttacker));
            if (isGameOver) break;
            yield return StartCoroutine(CombatLogicHandler(secondAttacker, firstAttacker));
        }
        StartPostGameActions();
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
        //FIXME: Randomize bot skin/species + stats should scale to match player stats otherwise it will become weak very fast
        bot.FighterConstructor(botName,
            Species.defaultStats[SpeciesNames.Orc]["hp"],
            Species.defaultStats[SpeciesNames.Orc]["damage"],
            Species.defaultStats[SpeciesNames.Orc]["speed"],
            SpeciesNames.Orc.ToString(), "Orc", 1, 0, 10, botCards);
    }

    IEnumerator CombatLogicHandler(Fighter attacker, Fighter defender)
    {
        // Move forward
        StartCoroutine(FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN));
        yield return StartCoroutine(movementScript.MoveForward(attacker, attacker.destinationPosition));

        // Attack

        int attackCounter = 0;

        while (!isGameOver && (attackCounter == 0 || attacktScript.IsAttackRepeated(attacker)))
        {
            yield return StartCoroutine(attacktScript.PerformAttack(attacker, defender));
            attackCounter++;
        };

        // Move back
        StartCoroutine(FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.RUN));
        FighterSkin.SwitchFighterOrientation(attacker.GetComponent<SpriteRenderer>());
        yield return StartCoroutine(movementScript.MoveBack(attacker, attacker.initialPosition));
        FighterSkin.SwitchFighterOrientation(attacker.GetComponent<SpriteRenderer>());
        StartCoroutine(FighterAnimations.ChangeAnimation(attacker, FighterAnimations.AnimationNames.IDLE));
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

    private void StartPostGameActions()
    {
        bool isPlayerWinner = PostGameActions.HasPlayerWon(player);
        int eloChange = MatchMaking.CalculateEloChange(User.Instance.elo, botElo, isPlayerWinner);
        int playerUpdatedExperience = player.experiencePoints + Levels.GetXpGain(isPlayerWinner);
        bool isLevelUp = Levels.IsLevelUp(player.level, playerUpdatedExperience);

        //PlayerData
        PostGameActions.SetElo(eloChange);
        PostGameActions.SetWinLoseCounter(isPlayerWinner);
        PostGameActions.SetExperience(player, isPlayerWinner);
        if (isLevelUp) PostGameActions.SetLevelUpSideEffects(player);
        EnergyManager.SubtractOneEnergyPoint();

        //Rewards
        PostGameActions.SetCurrencies(isPlayerWinner, isLevelUp);

        //UI
        fightersUIDataScript.SetResultsEloChange(eloChange);
        fightersUIDataScript.SetResultsLevelSlider(player.level, player.experiencePoints);
        fightersUIDataScript.SetResultsExpGainText(isPlayerWinner);
        fightersUIDataScript.ShowLevelUpIcon(isLevelUp);
        fightersUIDataScript.EnableResults(results);

        //Save
        PostGameActions.ResetPlayerHp(playerMaxHp);
        PostGameActions.Save(player);
    }

    private void ResetAnimationsState()
    {
        StartCoroutine(FighterAnimations.ChangeAnimation(player, FighterAnimations.AnimationNames.IDLE));
    }
}
