using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    const float DEFAULT_REFILL_TIME = 5f;
    float secondsUntilEnergyRefill = DEFAULT_REFILL_TIME;
    int maxEnergy = 3;
    Text energyValueText;

    void Start()
    {
        energyValueText = this.transform.Find("EnergyValue").GetComponent<Text>();

        // get all objects that belong to energy
        List<Transform> energyObjects = new List<Transform>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            energyObjects.Add(this.transform.GetChild(i));
        }

        Debug.Log(energyObjects.Count);
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
        int energyValue = int.Parse(energyValueText.text);

        if (energyValue != maxEnergy) energyValue++;

        energyValueText.text = energyValue.ToString();

        secondsUntilEnergyRefill = DEFAULT_REFILL_TIME;
    }
}

