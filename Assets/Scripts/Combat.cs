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
    List<int> fightersOrderOfAttack = new List<int> { };


    void Start()
    {
        InstantiateFighters();
        SetDestinationPositions();
        GenerateTestDataForFighters();
        SetOrderOfAttacks();
        StartCoroutine(InitiateCombat());
    }

    private void InstantiateFighters()
    {
        //Load fighter prefab. Resources.Load reads from /Resources path
        UnityEngine.Object fighterPrefab = Resources.Load("Fighter");
        playerGameObject = (GameObject)Instantiate(fighterPrefab, PLAYER_STARTING_POSITION, Quaternion.Euler(0, 0, 0));
        botGameObject = (GameObject)Instantiate(fighterPrefab, BOT_STARTING_POSITION, Quaternion.Euler(0, 0, 0));
    }

    private void SetDestinationPositions()
    {
        playerDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
        botDestinationPosition.x += DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
    }

    IEnumerator InitiateCombat()
    {
        while (!isGameOver)
        {
            //This foreach should contain every action of a player for that turn. E.G. Move, Attack, Throw skill....
            //FIXME: We need to separate the move forward and move back calls so we can insert the attacks in the middle
            foreach (int attackerId in fightersOrderOfAttack)
            {
                yield return StartCoroutine(CombatLogicHandler(attackerId));
            }
        }
    }

    private void GenerateTestDataForFighters()
    {
        // Why create dictionaries when we can simply do this?
        player = new Fighter("Eren", 10, 1, 3, "Fire", 10, 10);
        bot = new Fighter("Reiner", 10, 1, 6, "Leaf", 10, 10);
    }

    IEnumerator CombatLogicHandler(int id)
    {
        // From the current gameobject (this) access the movement component which is a script.
        Movement movementScript = this.GetComponent<Movement>();

        switch (id)
        {
            case 1:
                yield return StartCoroutine(movementScript.MoveForward(playerGameObject, playerDestinationPosition));
                yield return StartCoroutine(movementScript.MoveBack(playerGameObject, PLAYER_STARTING_POSITION));
                break;
            case 2:
                yield return StartCoroutine(movementScript.MoveForward(botGameObject, botDestinationPosition));
                yield return StartCoroutine(movementScript.MoveBack(botGameObject, BOT_STARTING_POSITION));
                break;
        }
    }

    // This method creates a dictionary with an ID for each fighter and sorts it afterwards by the speed of each fighter
    private void SetOrderOfAttacks()
    {
        OrderedDictionary fighterSpeedsDict = new OrderedDictionary
        {
            {1, player.speed},
            {2, bot.speed},
        };

        var fighterSpeedsSortedDict = fighterSpeedsDict.Cast<DictionaryEntry>()
                       .OrderByDescending(r => r.Value)
                       .ToDictionary(c => c.Key, d => d.Value);

        foreach (var id in fighterSpeedsSortedDict)
        {
            fightersOrderOfAttack.Add((int)id.Key);
        }
    }
}
