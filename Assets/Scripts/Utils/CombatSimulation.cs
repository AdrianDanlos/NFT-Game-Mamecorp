using UnityEngine;
using UnityEngine.UI;
//FIXME: Do we need this class?
public class CombatSimulation : MonoBehaviour
{
    // components
    Button button;

    // simlutation variables
    bool simulationResult;
    int WIN_XP = 2;
    int LOSE_XP = 1;

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
                LevelLogic.xp += WIN_XP;
            else
                LevelLogic.xp += LOSE_XP;
        }

        if (simulationResult)
            Debug.Log("Win! +" + WIN_XP + " exp."); 
        else
            Debug.Log("Lose! +" + LOSE_XP + " exp.");
    }

    bool HasEnergyAvailable(int energyAvailable)
    {
        return Energy.energyValue >= 1;
    }
}
