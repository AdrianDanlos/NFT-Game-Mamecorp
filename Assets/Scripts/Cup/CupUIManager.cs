using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CupUIManager : MonoBehaviour
{
    Transform labelContainer;

    private void Awake()
    {
        labelContainer = GameObject.Find("LabelContainer").GetComponent<Transform>();

        HideCupLabels();
    }

    private void Start()
    {
        ShowCupLabel();
    }

    private void HideCupLabels()
    {
        for(int i = 0; i < labelContainer.childCount; i++)
            labelContainer.GetChild(i).gameObject.SetActive(false);
    }

    private Transform GetCupLabelByName(string name)
    {
        switch(name)
        {
            case "FIRE":
                return labelContainer.GetChild(0);
            case "AIR":
                return labelContainer.GetChild(1);
            case "EARTH":
                return labelContainer.GetChild(2);
            case "WATER":
                return labelContainer.GetChild(3);
        }

        Debug.Log("Error!");
        return labelContainer.GetChild(0);
    }

    private void ShowCupLabel()
    {
        GetCupLabelByName(Cup.Instance.cupName).gameObject.SetActive(true);
    }
}
