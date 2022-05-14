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

    public void OnSelectFighter()
    {
        GameObject.FindGameObjectWithTag("FighterNamePopup").GetComponent<Canvas>().enabled = true;
        FighterSkinData fighterSkin = this.transform.Find("Fighter").GetComponent<FighterSkinData>();
        skinName = fighterSkin.skinName;
        species = fighterSkin.species;
    }

    public void OnConfirmFighterName()
    {
        fighterName = fighterNameInput.GetComponent<TextMeshProUGUI>().text;
        CreateFighterFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
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
