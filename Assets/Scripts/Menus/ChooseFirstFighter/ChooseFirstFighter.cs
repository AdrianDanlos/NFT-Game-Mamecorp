using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
using System.Collections.Generic;


public class ChooseFirstFighter : MonoBehaviour
{
    // Handles UI buttons
    public GameObject fighterNameInput;

    // scripts
    ChooseFirstFighterUI chooseFirstFighterUI;

    private void Awake()
    {
        chooseFirstFighterUI = GameObject.Find("UIManager").GetComponent<ChooseFirstFighterUI>();
    }

    public void OnSelectFighter()
    {
        switch (transform.name)
        {
            case "Container_Fighter_Left":
                SetFighter(transform.Find("Fighter_Left").GetComponent<FighterSkinData>());
                chooseFirstFighterUI.EnableLeftFighterHighlight();
                break;
            case "Container_Fighter_Mid":
                SetFighter(transform.Find("Fighter_Mid").GetComponent<FighterSkinData>());
                chooseFirstFighterUI.EnableMidFighterHighlight();
                break;
            case "Container_Fighter_Right":
                SetFighter(transform.Find("Fighter_Right").GetComponent<FighterSkinData>());
                chooseFirstFighterUI.EnableRightFighterHighlight();
                break;
        }
    }

    public void MoveToNextState()
    {
        switch (FirstPlayTempData.state.ToString())
        {
            case "FIGHTER":
                chooseFirstFighterUI.ChooseFighter();
                break;
            case "NAME":
                chooseFirstFighterUI.CheckName();
                break;
            case "COUNTRY":

                break;
        }
    }

    public void MoveToPreviousState()
    {
        switch (FirstPlayTempData.state.ToString())
        {
            case "FIGHTER":

                break;
            case "NAME":
                chooseFirstFighterUI.BackToChooseFighter();
                break;
            case "COUNTRY":

                break;
        }
    }

    private void SetFighter(FighterSkinData fighterSkin)
    {
        FirstPlayTempData.skinName = fighterSkin.skinName;
        FirstPlayTempData.species = fighterSkin.species;
    }

    public void OnConfirmFighterName()
    {
        FirstPlayTempData.fighterName = fighterNameInput.GetComponent<TextMeshProUGUI>().text;
        FirstPlayTempData.state = FirstPlayTempData.FirstPlayState.COUNTRY.ToString();
        CreateFighterFile(); // move to when all has been selected (country)
    }

    private void CreateFighterFile()
    {
        SpeciesNames speciesEnumMember = GeneralUtils.StringToSpeciesNamesEnum(FirstPlayTempData.species); 
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreatePlayerFighterInstance(
            FirstPlayTempData.fighterName, FirstPlayTempData.skinName, FirstPlayTempData.species,
            Species.defaultStats[speciesEnumMember]["hp"],
            Species.defaultStats[speciesEnumMember]["damage"],
            Species.defaultStats[speciesEnumMember]["speed"],
            new List<Skill>())));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);

        ResetAllPrefs();
    }

    private void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ResetRegexText()
    {
        chooseFirstFighterUI.regexText.gameObject.SetActive(false);
        chooseFirstFighterUI.regexText.text = "";
    }
}
