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

    // fighters
    private GameObject fighterLeft;
    private GameObject fighterMid;
    private GameObject fighterRight;
    private GameObject fighterLeftRing;
    private GameObject fighterMidRing;
    private GameObject fighterRightRing;
    private GameObject fighterLeftSpecieTitle;
    private GameObject fighterMidSpecieTitle;
    private GameObject fighterRightSpecieTitle;

    // stats per fighter
    private TextMeshProUGUI fighterLeftDamageText;
    private TextMeshProUGUI fighterLeftHpText;
    private TextMeshProUGUI fighterLeftSpeedText;

    private TextMeshProUGUI fighterMidDamageText;
    private TextMeshProUGUI fighterMidHpText;
    private TextMeshProUGUI fighterMidSpeedText;

    private TextMeshProUGUI fighterRightDamageText;
    private TextMeshProUGUI fighterRightHpText;
    private TextMeshProUGUI fighterRightSpeedText;

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

        fighterLeft = GameObject.Find("Container_Fighter_Left");
        fighterMid = GameObject.Find("Container_Fighter_Mid");
        fighterRight = GameObject.Find("Container_Fighter_Right");
        fighterLeftRing = GameObject.Find("Container_Fighter_Left/Ring");
        fighterMidRing = GameObject.Find("Container_Fighter_Mid/Ring");
        fighterRightRing = GameObject.Find("Container_Fighter_Right/Ring");
        fighterLeftSpecieTitle = GameObject.Find("Container_Fighter_Lrft/Specie");
        fighterMidSpecieTitle = GameObject.Find("Container_Fighter_Mid/Specie");
        fighterRightSpecieTitle = GameObject.Find("Container_Fighter_Right/Specie");

        chooseCountry = GameObject.Find("Canvas_Choose_Country").GetComponent<Canvas>();

        prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        next = GameObject.Find("Button_Next").GetComponent<Button>();
        panelInfo = GameObject.Find("Button_Prev").GetComponent<TextMeshProUGUI>();

        // Initial setup
        chooseName.enabled = false;
        chooseCountry.enabled = false;

        fighterLeft.SetActive(false);
        fighterMid.SetActive(false);
        fighterRight.SetActive(false);
        fighterLeftRing.SetActive(false);
        fighterMidRing.SetActive(false);
        fighterRightRing.SetActive(false);
        fighterLeftSpecieTitle.SetActive(false);
        fighterMidSpecieTitle.SetActive(false);
        fighterRightSpecieTitle.SetActive(false);
    }

    private void SetDefaultStats(string specie)
    {
        //Species.defaultStats[specie]["hp"];
        //Species.defaultStats[specie]["damage"];
        //Species.defaultStats[specie]["speed"];
    }

    public void ChooseFighter() 
    {
        // nextButtonGO.GetComponent<Button>().onClick.AddListener(() => OnClickNextHandler(isLevelUp));
    }

    public void OnExitNamePopUp()
    {
        GameObject.FindGameObjectWithTag("FighterNamePopup").GetComponent<Canvas>().enabled = false;
    }
}
