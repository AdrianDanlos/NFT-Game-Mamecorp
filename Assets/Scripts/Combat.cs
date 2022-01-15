using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject fighterOne;
    public GameObject fighterTwo;
    Vector3 FIGHTER_ONE_STARTING_POSITION = new Vector3(-8, 2, 0);
    Vector3 FIGHTER_TWO_STARTING_POSITION = new Vector3(8, 2, 0);
    float DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK = 1;

    Vector3 fighterOneDestinationPosition;
    Vector3 fighterTwoDestinationPosition;
    private bool isGameOver = false;


    void Start()
    {
        InstantiateFighters();
        SetDestinationPositions();
        StartCoroutine(InitiateCombat());
    }

    private void InstantiateFighters()
    {
        //Load fighter prefab. Resources.Load reads from /Resources path
        UnityEngine.Object fighterPrefab = Resources.Load("Fighter");
        fighterOne = (GameObject)Instantiate(fighterPrefab, FIGHTER_ONE_STARTING_POSITION, Quaternion.Euler(0, 0, 0));
        fighterTwo = (GameObject)Instantiate(fighterPrefab, FIGHTER_TWO_STARTING_POSITION, Quaternion.Euler(0, 0, 0));
    }

    private void SetDestinationPositions(){
        fighterOneDestinationPosition = FIGHTER_TWO_STARTING_POSITION;
        fighterOneDestinationPosition.x -= DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
        fighterTwoDestinationPosition = FIGHTER_ONE_STARTING_POSITION;
        fighterTwoDestinationPosition.x += DISTANCE_AWAY_FROM_EACHOTHER_ON_ATTACK;
    }

    IEnumerator InitiateCombat()
    {
        // From the current gameobject (this) access the movement component which is a script.
        Movement movementScript = this.GetComponent<Movement>();

        while (!isGameOver)
        {
            // Player A moving forward and backward
            yield return StartCoroutine(movementScript.MoveForward(fighterOne, fighterOneDestinationPosition));
            yield return StartCoroutine(movementScript.MoveBack(fighterOne, FIGHTER_ONE_STARTING_POSITION));

            // Player B moving forward and backward
            yield return StartCoroutine(movementScript.MoveForward(fighterTwo, fighterTwoDestinationPosition));
            yield return StartCoroutine(movementScript.MoveBack(fighterTwo, FIGHTER_TWO_STARTING_POSITION));
        }
    }
}
