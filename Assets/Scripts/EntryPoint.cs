using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;


public class EntryPoint : MonoBehaviour
{
    public static GameObject fighterGameObject;
    IEnumerator Start()
    {
        HideFighter();

        //TODO: Set the time of loading screen
        yield return new WaitForSeconds(0f);

        bool saveFilesFound = File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)) &&
            File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName));

        if (saveFilesFound)
        {
            ReadUserFile();
            ReadFighterFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        }

        else UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.ChooseFirstFighter.ToString());

        ChestTest();
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
            (float)fighterData["hp"], (float)fighterData["damage"], (float)fighterData["speed"], fighterData["skills"].ToObject<List<Skill>>(),
            (int)fighterData["level"], (int)fighterData["experiencePoints"]);
    }

    private void HideFighter()
    {
        fighterGameObject = GameObject.Find("Fighter");
        fighterGameObject.SetActive(false);
    }

    private void ChestTest()
    {
        int x = 0;
        int y = 0;
        int z = 0;
        int q = 0;

        for(int i=0; i < 10000; i++)
        {
            // reward mockup
            switch (Chest.GetBattleChestSkillReward(Chest.GetRandomBattleChest().ToString()).ToString())
            {
                case "COMMON":
                    x++;
                    break;
                case "UNCOMMON":
                    y++;
                    break;
                case "RARE":
                    z++;
                    break;
                case "EPIC":
                    q++;
                    break;
            }
        }

        Debug.Log("COMMON: " + x + "| UNCOMMON: " + y + " | RARE: " + z + " | EPIC: " + q);
    }
}