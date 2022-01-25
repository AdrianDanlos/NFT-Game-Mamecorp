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
    public GameObject playerWrapperGameObject;
    public GameObject botGameObject;

    // Positions data
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
        //FIXME: Move this calls to another scene
        string botName = MatchMaking.FetchBotRandomName();
        int botElo = MatchMaking.GenerateBotElo(400);
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

        //FIXME: This is for test purposes. Assign the cards properly in the future
        player.cards = bot.cards;

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
        botDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
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
