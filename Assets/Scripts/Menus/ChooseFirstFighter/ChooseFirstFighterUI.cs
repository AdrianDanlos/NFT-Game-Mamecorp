using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFirstFighterUI : MonoBehaviour
{
    // UI
    // canvases
    private Canvas chooseFighter;
    private Canvas chooseName;
    private Canvas chooseCountry;

    private Button prev;
    private Button next;
    private TextMeshProUGUI panelInfo;

    private void Awake()
    {
        HandleUIOnAwake();
    }

    private void HandleUIOnAwake()
    {
        chooseFighter = GameObject.Find("Canvas_Choose_Fighter").GetComponent<Canvas>();
        chooseName = GameObject.Find("Canvas_Choose_Name_Fighter").GetComponent<Canvas>();
        chooseCountry = GameObject.Find("Canvas_Choose_Country").GetComponent<Canvas>();

        prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        next = GameObject.Find("Button_Next").GetComponent<Button>();
        panelInfo = GameObject.Find("Button_Prev").GetComponent<TextMeshProUGUI>();

        // Initial setup
        chooseName.enabled = false;
        chooseCountry.enabled = false;
    }

    public void OnExitNamePopUp()
    {
        GameObject.FindGameObjectWithTag("FighterNamePopup").GetComponent<Canvas>().enabled = false;
    }
}
