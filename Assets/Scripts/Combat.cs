using System.Collections;
using UnityEngine;


public class Combat : MonoBehaviour
{
    // Data Objects
    public Fighter fighterOne;
    public Fighter fighterTwo;

    // GameObjects related data
    public GameObject fighterOneGameObject;
    public GameObject fighterTwoGameObject;
    Vector3 FIGHTER_ONE_STARTING_POSITION = new Vector3(-8, 2, 0);
    Vector3 FIGHTER_TWO_STARTING_POSITION = new Vector3(8, 2, 0);
    float DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK = 1;
    Vector3 fighterOneDestinationPosition;
    Vector3 fighterTwoDestinationPosition;

    // Game status data
    private bool isGameOver = false;
    int[] fightersOrderOfAttack;


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
        fighterOneDestinationPosition = FIGHTER_TWO_STARTING_POSITION;
        fighterOneDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
        fighterTwoDestinationPosition = FIGHTER_ONE_STARTING_POSITION;
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
        fighterOne = new Fighter("Eren", 10, 1, 1, "Fire", 10, 10);
        fighterTwo = new Fighter("Reiner", 10, 1, 2, "Leaf", 10, 10);
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

    private void SetOrderOfAttacks()
    {
        fightersOrderOfAttack = fighterOne.speed > fighterTwo.speed ? new int[] { 1, 2 } : new int[] { 2, 1 };
    }
}
