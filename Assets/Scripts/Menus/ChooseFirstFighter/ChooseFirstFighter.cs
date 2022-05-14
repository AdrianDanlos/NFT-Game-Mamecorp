using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
using System.Collections.Generic;


public class ChooseFirstFighter : MonoBehaviour
{
    // Handles UI buttons
    public GameObject fighterNameInput;
    private string fighterName;
    private static string skinName;
    private static string species;

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

    private void SetFighter(FighterSkinData fighterSkin)
    {
        FirstPlayTempData.skinName = fighterSkin.skinName;
        FirstPlayTempData.species = fighterSkin.species;
    }

    public void OnConfirmFighterName()
    {
        fighterName = fighterNameInput.GetComponent<TextMeshProUGUI>().text;
        CreateFighterFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.EntryPoint.ToString());
    }

    private void CreateFighterFile()
    {
        SpeciesNames speciesEnumMember = GeneralUtils.StringToSpeciesNamesEnum(species); 
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreatePlayerFighterInstance(
            fighterName, skinName, species,
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
}
