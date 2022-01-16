using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;



public class Combat : MonoBehaviour
{
    // Data Objects
    public Fighter fighterOne;
    public Fighter fighterTwo;

    // GameObjects related data
    public GameObject fighterOneGameObject;
    public GameObject fighterTwoGameObject;
    static Vector3 FIGHTER_ONE_STARTING_POSITION = new Vector3(-8, 2, 0);
    static Vector3 FIGHTER_TWO_STARTING_POSITION = new Vector3(8, 2, 0);
    float DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK = 1;
    Vector3 fighterOneDestinationPosition = FIGHTER_TWO_STARTING_POSITION;
    Vector3 fighterTwoDestinationPosition = FIGHTER_ONE_STARTING_POSITION;

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
        fighterOneGameObject = (GameObject)Instantiate(fighterPrefab, FIGHTER_ONE_STARTING_POSITION, Quaternion.Euler(0, 0, 0));
        fighterTwoGameObject = (GameObject)Instantiate(fighterPrefab, FIGHTER_TWO_STARTING_POSITION, Quaternion.Euler(0, 0, 0));
    }

    private void SetDestinationPositions()
    {
        fighterOneDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
        fighterTwoDestinationPosition.x += DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
    }

    IEnumerator InitiateCombat()
    {
        while (!isGameOver)
        {
            //This foreach should contain every action of a player for that turn. E.G. Move, Attack, Throw skill....
            //FIXME: We need to separate the move forward and move back calls so we can insert the attacks in the middle
            foreach (int attackerId in fightersOrderOfAttack)
            {
                yield return StartCoroutine(MoveFighter(attackerId));
            }
        }
    }

    private void GenerateTestDataForFighters()
    {
        // Why create dictionaries when we can simply do this?
        fighterOne = new Fighter("Eren", 10, 1, 3, "Fire", 10, 10);
        fighterTwo = new Fighter("Reiner", 10, 1, 6, "Leaf", 10, 10);
    }

    IEnumerator MoveFighter(int id)
    {
        // From the current gameobject (this) access the movement component which is a script.
        Movement movementScript = this.GetComponent<Movement>();

        switch (id)
        {
            case 1:
                yield return StartCoroutine(movementScript.MoveForward(fighterOneGameObject, fighterOneDestinationPosition));
                yield return StartCoroutine(movementScript.MoveBack(fighterOneGameObject, FIGHTER_ONE_STARTING_POSITION));
                break;
            case 2:
                yield return StartCoroutine(movementScript.MoveForward(fighterTwoGameObject, fighterTwoDestinationPosition));
                yield return StartCoroutine(movementScript.MoveBack(fighterTwoGameObject, FIGHTER_TWO_STARTING_POSITION));
                break;
        }
    }

    // This method creates a dictionary with an ID for each fighter and sorts it afterwards by the speed of each fighter
    private void SetOrderOfAttacks()
    {
        OrderedDictionary fighterSpeedsDict = new OrderedDictionary
        {
            {1, fighterOne.speed},
            {2, fighterTwo.speed},
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
