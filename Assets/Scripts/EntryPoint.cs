using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class EntryPoint : MonoBehaviour
{
    public static GameObject fighterGameObject;
    private void Awake()
    {
        fighterGameObject = GameObject.Find("Fighter");
        fighterGameObject.SetActive(false);

        if (File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)) && File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName)))
        {
            ReadUserFile();
            ReadFighterFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Combat");
        }
        else UnityEngine.SceneManagement.SceneManager.LoadScene("UserFirstStart");
    }

    private static void ReadUserFile()
    {
        JObject userData = JsonDataManager.ReadData(JsonDataManager.UserFileName);
        UserFactory.CreateUserInstance((string)userData["userName"], (int)userData["wins"], (int)userData["loses"], (int)userData["elo"]);
    }
    private static void ReadFighterFile()
    {
        JObject fighterData = JsonDataManager.ReadData(JsonDataManager.FighterFileName);
        FighterFactory.CreateFighterInstance((string)fighterData["fighterName"], (float)fighterData["hp"], (float)fighterData["damage"], (float)fighterData["speed"],
            (string)fighterData["species"], (int)fighterData["level"], (int)fighterData["manaSlots"], fighterData["cards"].ToObject<List<Card>>());
    }
}