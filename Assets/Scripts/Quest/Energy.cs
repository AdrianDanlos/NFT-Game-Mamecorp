using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    const int BASE_ENERGY = 1;
    const float DEFAULT_REFILL_TIME = 2f;
    float secondsUntilEnergyRefill = DEFAULT_REFILL_TIME;
    int maxEnergy = 3;
    Text energyValueText;
    static int energyValue;

    void Start()
    {
        energyValueText = this.transform.Find("EnergyValue").GetComponent<Text>();
        energyValue = BASE_ENERGY;
        energyValueText.text = energyValue.ToString();

        // get all objects that belong to energy
        List<Transform> energyObjects = new List<Transform>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            energyObjects.Add(this.transform.GetChild(i));
        }
    }

    void Update()
    {
        secondsUntilEnergyRefill -= Time.deltaTime;
        this.transform.Find("RefillTimerValue").GetComponent<Text>().text = ((int)secondsUntilEnergyRefill).ToString();

        if (secondsUntilEnergyRefill <= 0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        if (energyValue != maxEnergy) energyValue++;

        energyValueText.text = energyValue.ToString();

        secondsUntilEnergyRefill = DEFAULT_REFILL_TIME;
    }
}

