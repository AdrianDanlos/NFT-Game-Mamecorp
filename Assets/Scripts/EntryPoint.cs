using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class EntryPoint : MonoBehaviour
{
    public static GameObject fighterGameObject;
    private void Awake()
    {
        HideFighter();

        if (File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)) &&
         File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName)))
        {
            ReadUserFile();
            ReadFighterFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        }
        else UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.UserFirstStart.ToString());
    }

    private void ReadUserFile()
    {
        JObject userData = JsonDataManager.ReadData(JsonDataManager.UserFileName);
        UserFactory.CreateUserInstance((string)userData["userName"], (int)userData["energy"], (int)userData["wins"], (int)userData["loses"], (int)userData["elo"], (int)userData["gold"], (int)userData["gems"]);
    }
    private void ReadFighterFile()
    {
        JObject fighterData = JsonDataManager.ReadData(JsonDataManager.FighterFileName);
        FighterFactory.CreatePlayerFighterInstance((string)fighterData["fighterName"], (string)fighterData["skin"], (string)fighterData["species"],
            (float)fighterData["hp"], (float)fighterData["damage"], (float)fighterData["speed"], (int)fighterData["level"],
            (int)fighterData["experiencePoints"], fighterData["cards"].ToObject<List<Card>>());
    }

    private void HideFighter()
    {
        fighterGameObject = GameObject.Find("Fighter");
        fighterGameObject.SetActive(false);
    }
}