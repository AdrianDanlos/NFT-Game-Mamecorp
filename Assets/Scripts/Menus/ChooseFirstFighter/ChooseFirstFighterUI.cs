using System;
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
    private Transform fighterLeft;
    private Transform fighterMid;
    private Transform fighterRight;
    private Transform fighterLeftObject;
    private Transform fighterMidObject;
    private Transform fighterRightObject;
    private Transform fighterLeftRing;
    private Transform fighterMidRing;
    private Transform fighterRightRing;
    private Transform fighterLeftSpecieTitle;
    private Transform fighterMidSpecieTitle;
    private Transform fighterRightSpecieTitle;

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

        fighterLeft = GameObject.Find("Container_Fighter_Left").GetComponent<Transform>();
        fighterMid = GameObject.Find("Container_Fighter_Mid").GetComponent<Transform>();
        fighterRight = GameObject.Find("Container_Fighter_Right").GetComponent<Transform>();
        fighterLeftObject = GameObject.Find("Fighter_Left").GetComponent<Transform>();
        fighterMidObject = GameObject.Find("Fighter_Mid").GetComponent<Transform>();
        fighterRightObject = GameObject.Find("Fighter_Right").GetComponent<Transform>();
        fighterLeftRing = GameObject.Find("Ring_Left").GetComponent<Transform>();
        fighterMidRing = GameObject.Find("Ring_Mid").GetComponent<Transform>();
        fighterRightRing = GameObject.Find("Ring_Right").GetComponent<Transform>();
        fighterLeftSpecieTitle = GameObject.Find("Specie_Left").GetComponent<Transform>();
        fighterMidSpecieTitle = GameObject.Find("Specie_Mid").GetComponent<Transform>();
        fighterRightSpecieTitle = GameObject.Find("Specie_Right").GetComponent<Transform>();

        chooseCountry = GameObject.Find("Canvas_Choose_Country").GetComponent<Canvas>();

        prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        next = GameObject.Find("Button_Next").GetComponent<Button>();
        panelInfo = GameObject.Find("Button_Prev").GetComponent<TextMeshProUGUI>();

        fighterLeftDamageText = GameObject.Find("Attack_Value_Left").GetComponent<TextMeshProUGUI>();
        fighterLeftHpText = GameObject.Find("Life_Value_Left").GetComponent<TextMeshProUGUI>();
        fighterLeftSpeedText = GameObject.Find("Speed_Value_Left").GetComponent<TextMeshProUGUI>();

        // setup stats
        SetDefaultStats(GameObject.Find("Fighter_Left").GetComponent<FighterSkinData>().species);
        SetDefaultStats(GameObject.Find("Fighter_Mid").GetComponent<FighterSkinData>().species);
        SetDefaultStats(GameObject.Find("Fighter_Right").GetComponent<FighterSkinData>().species);

        // Initial setup
        chooseName.enabled = false;
        chooseCountry.enabled = false;

        fighterLeftRing.gameObject.SetActive(false);
        fighterMidRing.gameObject.SetActive(false);
        fighterRightRing.gameObject.SetActive(false);
        fighterLeftSpecieTitle.gameObject.SetActive(false);
        fighterMidSpecieTitle.gameObject.SetActive(false);
        fighterRightSpecieTitle.gameObject.SetActive(false);
    }

    private void SetDefaultStats(string specie)
    {
        fighterLeftDamageText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["damage"].ToString();
        fighterLeftHpText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["hp"].ToString();
        fighterLeftSpeedText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["speed"].ToString();
    }

    public void EnableLeftFighterHighlight()
    {
        DisableMidFighterHighlight();
        DisableRightFighterHighlight();
        fighterLeftObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        fighterLeftRing.gameObject.SetActive(true);
        fighterLeftSpecieTitle.gameObject.SetActive(true);
    }

    public void EnableMidFighterHighlight()
    {
        DisableLeftFighterHighlight();
        DisableRightFighterHighlight();
        fighterMidObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        fighterMidRing.gameObject.SetActive(true);
        fighterMidSpecieTitle.gameObject.SetActive(true);
    }

    public void EnableRightFighterHighlight()
    {
        DisableLeftFighterHighlight();
        DisableMidFighterHighlight();
        fighterRightObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        fighterRightRing.gameObject.SetActive(true);
        fighterRightSpecieTitle.gameObject.SetActive(true);
    }

    public void DisableLeftFighterHighlight()
    {
        fighterRightObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterMidObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterRightRing.gameObject.SetActive(false);
        fighterRightSpecieTitle.gameObject.SetActive(false);
        fighterMidRing.gameObject.SetActive(false);
        fighterMidSpecieTitle.gameObject.SetActive(false);
    }

    public void DisableMidFighterHighlight()
    {
        fighterLeftObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterRightObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterLeftRing.gameObject.SetActive(false);
        fighterLeftSpecieTitle.gameObject.SetActive(false);
        fighterRightRing.gameObject.SetActive(false);
        fighterRightSpecieTitle.gameObject.SetActive(false);
    }

    public void DisableRightFighterHighlight()
    {
        fighterLeftObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterMidObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterLeftRing.gameObject.SetActive(false);
        fighterLeftSpecieTitle.gameObject.SetActive(false);
        fighterMidRing.gameObject.SetActive(false);
        fighterMidSpecieTitle.gameObject.SetActive(false);
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
