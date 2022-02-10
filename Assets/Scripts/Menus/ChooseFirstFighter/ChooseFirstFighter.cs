using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class ChooseFirstFighter : MonoBehaviour
{
    public void OnSelectFighter()
    {
        CreateFighterFile();
        //FIXME: Redirect to main menu after user has given a name to the fighter
        //Send selected skin to the fighter constructor
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void CreateFighterFile()
    {
        //TODO: Pedir nombre al usuario en escena
        string fighterName = "fighterNameTypedByUser";
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreatePlayerFighterInstance(fighterName)));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
    }
}
