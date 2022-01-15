using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject fighterOne;

    void Start()
    {
        InstantiateFighters();
        StartCoroutine(InitiateCombat());
    }

    private void InstantiateFighters()
    {
        //Load fighter prefab. Resources.Load reads from /Resources path
        UnityEngine.Object fighterPrefab = Resources.Load("Fighter");
        fighterOne = (GameObject)Instantiate(fighterPrefab, new Vector3(-8, 2, 0), Quaternion.Euler(0, 0, 0));
    }

    IEnumerator InitiateCombat()
    {
        // From the current gameobject (this) access the movement component which is a script.
        Movement movementScript = this.GetComponent<Movement>();
        yield return StartCoroutine(movementScript.MoveForward(fighterOne, new Vector3(8, 1, 1)));
    }
}
