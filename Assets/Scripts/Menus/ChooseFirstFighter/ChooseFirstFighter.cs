using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
using System;
using System.Reflection;
using System.Linq;

public class ChooseFirstFighter : MonoBehaviour
{
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
        SpeciesNames speciesEnumMember = (SpeciesNames)Enum.Parse(typeof(SpeciesNames), species); //Monster, Robot, Alien
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreatePlayerFighterInstance(
            fighterName, skinName, species, 
            Species.defaultStats[speciesEnumMember]["hp"], 
            Species.defaultStats[speciesEnumMember]["damage"],
            Species.defaultStats[speciesEnumMember]["speed"])));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
    }
}
