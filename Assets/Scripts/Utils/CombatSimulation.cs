using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSimulation : MonoBehaviour
{
    Button button;
    bool simulationResult;

    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(SimulateFight);
    }

    void Update()
    {
        if (!HasEnergyAvailable(Energy.energyValue))
            button.interactable = false;
        else
            button.interactable = true;
    }

    bool GenerateFightResult()
    {
        // true win
        // false lost
        return Random.Range(0, 2) == 1;
    }

    void SimulateFight()
    {
        if (HasEnergyAvailable(Energy.energyValue))
        {
            Energy.energyValue -= 1;

            simulationResult = GenerateFightResult();
            if (simulationResult)
                LevelLogic.xp += 2;
            else
                LevelLogic.xp += 1;

            Debug.Log(simulationResult);
        }
    }

    // is this function needed as we use same static value twice?
    bool HasEnergyAvailable(int energyAvailable)
    {
        return Energy.energyValue >= 1;
    }
}
