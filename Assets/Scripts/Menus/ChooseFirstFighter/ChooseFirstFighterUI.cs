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
    public Canvas chooseFighter;
    public Canvas chooseName;
    public Canvas chooseCountry;

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

    // animations
    private SetFighterAnimations fighterLeftAnimator;
    private SetFighterAnimations fighterMidAnimator;
    private SetFighterAnimations fighterRightAnimator;

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
    public TextMeshProUGUI panelInfo;

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

        fighterLeftAnimator = GameObject.Find("Fighter_Left").GetComponent<SetFighterAnimations>();
        fighterMidAnimator = GameObject.Find("Fighter_Mid").GetComponent<SetFighterAnimations>();
        fighterRightAnimator = GameObject.Find("Fighter_Right").GetComponent<SetFighterAnimations>();

        chooseCountry = GameObject.Find("Canvas_Choose_Country").GetComponent<Canvas>();

        prev = GameObject.Find("Button_Prev").GetComponent<Button>();
        next = GameObject.Find("Button_Next").GetComponent<Button>();
        panelInfo = GameObject.Find("PanelControlTitle").GetComponent<TextMeshProUGUI>();

        fighterLeftDamageText = GameObject.Find("Attack_Value_Left").GetComponent<TextMeshProUGUI>();
        fighterLeftHpText = GameObject.Find("Life_Value_Left").GetComponent<TextMeshProUGUI>();
        fighterLeftSpeedText = GameObject.Find("Speed_Value_Left").GetComponent<TextMeshProUGUI>();
        fighterMidDamageText = GameObject.Find("Attack_Value_Mid").GetComponent<TextMeshProUGUI>();
        fighterMidHpText = GameObject.Find("Life_Value_Mid").GetComponent<TextMeshProUGUI>();
        fighterMidSpeedText = GameObject.Find("Speed_Value_Mid").GetComponent<TextMeshProUGUI>();
        fighterRightDamageText = GameObject.Find("Attack_Value_Right").GetComponent<TextMeshProUGUI>();
        fighterRightHpText = GameObject.Find("Life_Value_Right").GetComponent<TextMeshProUGUI>();
        fighterRightSpeedText = GameObject.Find("Speed_Value_Right").GetComponent<TextMeshProUGUI>();

        // setup stats
        SetDefaultStats(GameObject.Find("Fighter_Left").GetComponent<FighterSkinData>().species, "Fighter_Left");
        SetDefaultStats(GameObject.Find("Fighter_Mid").GetComponent<FighterSkinData>().species, "Fighter_Mid");
        SetDefaultStats(GameObject.Find("Fighter_Right").GetComponent<FighterSkinData>().species, "Fighter_Right");

        // Initial setup
        chooseName.enabled = false;
        chooseCountry.enabled = false;

        fighterLeftObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterMidObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);
        fighterRightObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(90, 90, 90, 255);

        fighterLeftRing.gameObject.SetActive(false);
        fighterMidRing.gameObject.SetActive(false);
        fighterRightRing.gameObject.SetActive(false);
        fighterLeftSpecieTitle.gameObject.SetActive(false);
        fighterMidSpecieTitle.gameObject.SetActive(false);
        fighterRightSpecieTitle.gameObject.SetActive(false);

        prev.gameObject.SetActive(false);
        next.gameObject.SetActive(false);

        // set canvas state
        FirstPlayTempData.state = FirstPlayTempData.FirstPlayState.FIGHTER.ToString();
    }

    private void SetDefaultStats(string specie, string fighter)
    {
        switch (fighter)
        {
            case "Fighter_Left":
                fighterLeftDamageText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["damage"].ToString();
                fighterLeftHpText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["hp"].ToString();
                fighterLeftSpeedText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["speed"].ToString();
                break;
            case "Fighter_Mid":
                fighterMidDamageText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["damage"].ToString();
                fighterMidHpText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["hp"].ToString();
                fighterMidSpeedText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["speed"].ToString();
                break;
            case "Fighter_Right":
                fighterRightDamageText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["damage"].ToString();
                fighterRightHpText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["hp"].ToString();
                fighterRightSpeedText.text = Species.defaultStats[(SpeciesNames)Enum.Parse(typeof(SpeciesNames), specie)]["speed"].ToString();
                break;
        }
    }

    public void EnableLeftFighterHighlight()
    {
        next.gameObject.SetActive(true);
        fighterLeftAnimator.PlayRunAnimation();
        fighterMidAnimator.PlayIdleAnimation();
        fighterRightAnimator.PlayIdleAnimation();
        DisableMidFighterHighlight();
        DisableRightFighterHighlight();
        fighterLeftObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        fighterLeftRing.gameObject.SetActive(true);
        fighterLeftSpecieTitle.gameObject.SetActive(true);
    }

    public void EnableMidFighterHighlight()
    {
        next.gameObject.SetActive(true);
        fighterLeftAnimator.PlayIdleAnimation();
        fighterMidAnimator.PlayRunAnimation();
        fighterRightAnimator.PlayIdleAnimation();
        DisableLeftFighterHighlight();
        DisableRightFighterHighlight();
        fighterMidObject.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        fighterMidRing.gameObject.SetActive(true);
        fighterMidSpecieTitle.gameObject.SetActive(true);
    }

    public void EnableRightFighterHighlight()
    {
        next.gameObject.SetActive(true);
        fighterLeftAnimator.PlayIdleAnimation();
        fighterMidAnimator.PlayIdleAnimation();
        fighterRightAnimator.PlayRunAnimation();
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

    public void OnExitNamePopUp()
    {
        GameObject.FindGameObjectWithTag("FighterNamePopup").GetComponent<Canvas>().enabled = false;
    }

    public void EnableNextBtn()
    {
        next.gameObject.SetActive(true);
    }

    public void EnablePrevBtn()
    {
        prev.gameObject.SetActive(true);
    }

    public void DisableNextBtn()
    {
        next.gameObject.SetActive(false);
    }

    public void DisablePrevBtn()
    {
        prev.gameObject.SetActive(false);
    }
}
