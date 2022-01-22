using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSimulation : MonoBehaviour
{
    Button button;
    int simulationResult;

    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(SimulateFight);
    }

    bool GenerateFightResult()
    {
        // true win
        // false lost
        return Random.Range(0, 2) == 1;
    }

    void SimulateFight()
    {
        Debug.Log(GenerateFightResult());
    }

}
