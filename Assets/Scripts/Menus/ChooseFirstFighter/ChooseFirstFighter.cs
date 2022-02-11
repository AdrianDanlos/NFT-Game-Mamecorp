using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;

public class ChooseFirstFighter : MonoBehaviour
{
    public GameObject fighterNameInput;
    public Animator fighterOne;
    // public Animator fighterTwo;
    // public Animator fighterThree;

    private void Start() {
        fighterOne = GameObject.FindGameObjectWithTag("StarterFighterOne").GetComponent<Animator>();
        //ChooseFirstFighterAnimations.SetFightersSkin(fighterOne);
    }
    public void OnSelectFighter()
    {
        //FIXME: Save skin
        GameObject.FindGameObjectWithTag("FighterNamePopup").GetComponent<Canvas>().enabled = true;
    }

    public void OnConfirmFighterName()
    {
        string fighterName = fighterNameInput.GetComponent<TextMeshProUGUI>().text;
        //FIXME: Send selected skin to the fighter constructor
        CreateFighterFile(fighterName);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void CreateFighterFile(string fighterName)
    {
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreatePlayerFighterInstance(fighterName)));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
    }
}
