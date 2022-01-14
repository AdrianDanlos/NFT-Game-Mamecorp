using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    float targetTime = 5f;
    int maxEnergy = 3;

    void Start()
    {
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
        targetTime -= Time.deltaTime;
        this.transform.Find("RefillTimerValue").GetComponent<Text>().text = ((int) targetTime).ToString();

        if (targetTime <= 0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        int energyValue = int.Parse(this.transform.Find("EnergyValue").GetComponent<Text>().text);

        if (energyValue != maxEnergy) energyValue++;

        this.transform.Find("EnergyValue").GetComponent<Text>().text = energyValue.ToString();

        targetTime = 5f;
    }
}

