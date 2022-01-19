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


    void Start()
    {
        InstantiateFightersGameObjects();
        AddScriptComponentToFighters();
        SetDestinationPositions();
        GenerateTestDataForFighters();
        SetOrderOfAttacks();
        StartCoroutine(InitiateCombat());
    }

    private void InstantiateFightersGameObjects()
    {
        //Load fighter prefab. Resources.Load reads from /Resources path
        UnityEngine.Object fighterPrefab = Resources.Load("Fighter");
        playerGameObject = (GameObject)Instantiate(fighterPrefab, PLAYER_STARTING_POSITION, Quaternion.identity,
            GameObject.FindGameObjectWithTag("Canvas").transform);
        botGameObject = (GameObject)Instantiate(fighterPrefab, BOT_STARTING_POSITION, Quaternion.identity,
            GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    private void SetDestinationPositions()
    {
        playerDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
        botDestinationPosition.x += DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
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
    }

    private void GenerateTestDataForFighters()
    {
        // Get all the existing cards and add them to the list of cards of the fighter
        List<OrderedDictionary> cardCollection = CardCollection.cards;
        List<Card> playerCards = new List<Card>();
        List<Card> botCards = new List<Card>();

        foreach (OrderedDictionary card in cardCollection)
        {
            Card cardInstance = new Card((string)card["cardName"], (int)card["mana"], (string)card["text"], (string)card["rarity"], (string)card["type"]);
            playerCards.Add(cardInstance);
            botCards.Add(cardInstance);
        }

        player.FighterConstructor("Eren", 10, 1, 3, "Fire", 10, 10, playerCards, PLAYER_STARTING_POSITION, playerDestinationPosition);
        bot.FighterConstructor("Reiner", 10, 1, 6, "Leaf", 10, 10, botCards, BOT_STARTING_POSITION, botDestinationPosition);
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

    private void AddScriptComponentToFighters()
    {
        player = playerGameObject.AddComponent<Fighter>();
        bot = botGameObject.AddComponent<Fighter>();
    }
}
