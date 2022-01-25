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

    // GameObjects related data
    public GameObject playerGameObject;
    public GameObject botGameObject;
    static Vector3 PLAYER_STARTING_POSITION = new Vector3(-8, 2, 0);
    static Vector3 BOT_STARTING_POSITION = new Vector3(8, 2, 0);
    float DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK = 1;
    Vector3 playerDestinationPosition = BOT_STARTING_POSITION;
    Vector3 botDestinationPosition = PLAYER_STARTING_POSITION;

    // Game status data
    private bool isGameOver = false;
    List<Fighter> fightersOrderOfAttack = new List<Fighter> { };

    private void Awake()
    {
        //Move this calls to another scene
        string botName = MatchMaking.FetchBotRandomName();
        int botElo = MatchMaking.GenerateBotElo(400);
    }
    void Start()
    {
        EnablePlayerGameObject();
        InstantiateBotGameObject();
        AttachScriptToFighterGameObjects();
        GenerateBotData();
        SetFighterPositions();
        SetOrderOfAttacks();

        //FIXME: This is for test purposes. Assign the cards properly in the future
        player.cards = bot.cards;

        StartCoroutine(InitiateCombat());
    }

    private void AttachScriptToFighterGameObjects()
    {
        //For the player it is already added since the moment we start the application. We only need to get the component.
        player = playerGameObject.GetComponent<Fighter>();
        bot = botGameObject.AddComponent<Fighter>();
    }

    private void EnablePlayerGameObject()
    {
        //The fighter gameobject for the player is created on application start but kept inactive. Its simply a data container outside of the combat scene.
        playerGameObject = GameObject.Find("FighterWrapper").transform.Find("Fighter").gameObject;
        playerGameObject.SetActive(true);
    }

    private void InstantiateBotGameObject()
    {
        //Load fighter prefab. Resources.Load reads from /Resources path
        UnityEngine.Object fighterPrefab = Resources.Load("Fighter");
        botGameObject = (GameObject)Instantiate(fighterPrefab, BOT_STARTING_POSITION, Quaternion.identity,
            GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    private void SetFighterPositions()
    {
        player.initialPosition = PLAYER_STARTING_POSITION;
        player.destinationPosition = playerDestinationPosition;
        playerDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;

        bot.initialPosition = BOT_STARTING_POSITION;
        bot.destinationPosition = botDestinationPosition;
        botDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
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

        //TODO: Send the correct values here
        PostGameActions.UpdateElo(400, 430, true);
    }

    private void GenerateBotData()
    {
        // Get all the existing cards and add them to the list of cards of the fighter
        List<OrderedDictionary> cardCollection = CardCollection.cards;
        List<Card> botCards = new List<Card>();

        foreach (OrderedDictionary card in cardCollection)
        {
            Card cardInstance = new Card((string)card["cardName"], (int)card["mana"], (string)card["text"], (string)card["rarity"], (string)card["type"]);
            botCards.Add(cardInstance);
        }
        bot.FighterConstructor("Reiner", 10, 1, 6, "Leaf", 1, 10, botCards);
    }

    IEnumerator CombatLogicHandler(Fighter attacker, Fighter defender)
    {
        // From the current gameobject (this) access the movement component which is a script.
        Movement movementScript = this.GetComponent<Movement>();
        Attack attacktScript = this.GetComponent<Attack>();

        // Move forward
        yield return StartCoroutine(movementScript.MoveForward(attacker, attacker.destinationPosition));

        // Attack
        int attackCounter = 0;

        while (!isGameOver && (attackCounter == 0 || attacktScript.IsAttackRepeated(attacker)) && !attacktScript.IsAttackDodged(defender))
        {
            attacktScript.DealDamage(attacker, defender);
            isGameOver = defender.hp <= 0 ? true : false;
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
