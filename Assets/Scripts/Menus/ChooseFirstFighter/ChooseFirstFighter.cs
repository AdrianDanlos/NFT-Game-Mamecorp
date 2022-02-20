using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;

public class ChooseFirstFighter : MonoBehaviour
{
    public GameObject fighterNameInput;
    private string fighterName;
    private static string skinName;
    public void OnSelectFighter()
    {
        GameObject.FindGameObjectWithTag("FighterNamePopup").GetComponent<Canvas>().enabled = true;
        skinName = this.transform.Find("Fighter").GetComponent<FighterSkinName>().skinName;
        Debug.Log(skinName);
    }

    public void OnConfirmFighterName()
    {
        fighterName = fighterNameInput.GetComponent<TextMeshProUGUI>().text;
        CreateFighterFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void CreateFighterFile()
    {
        Debug.Log(skinName);
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreatePlayerFighterInstance(fighterName, skinName)));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
    }
}
