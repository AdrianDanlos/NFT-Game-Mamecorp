using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSimulation : MonoBehaviour
{
    Button button;
    static bool simulationResult;

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
        simulationResult = GenerateFightResult();
        if (simulationResult)
        {
            LevelLogic.xp += 2;
        } else
        {
            LevelLogic.xp += 1;
        }

        Debug.Log(simulationResult);
    }

}
