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
        ReadOrCreateUserFile();
        ReadOrCreateFighterFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Combat");
    }

    public static void ReadOrCreateUserFile()
    {
        if (File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)))
        {
            JObject userData = JsonDataManager.ReadData(JsonDataManager.UserFileName);
            CreateUserInstance((string)userData["userName"], (int)userData["wins"], (int)userData["loses"], (int)userData["elo"]);
        }
        else
        {
            //TODO: Pedir nombre al usuario en escena
            string userName = "userNameTypedByUser";
            CreateUserInstance(userName);
            JObject user = JObject.FromObject(User.Instance);
            JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
        };
    }

    public static void ReadOrCreateFighterFile()
    {
        if (File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName)))
        {
            JObject fighterData = JsonDataManager.ReadData(JsonDataManager.FighterFileName);
            CreateFighterInstance((string)fighterData["fighterName"], (float)fighterData["hp"], (float)fighterData["damage"], (float)fighterData["speed"],
                (string)fighterData["species"], (int)fighterData["level"], (int)fighterData["manaSlots"], fighterData["cards"].ToObject<List<Card>>());
        }
        else
        {
            //TODO: Pedir nombre al usuario en escena
            string fighterName = "fighterNameTypedByUser";
            JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(CreateFighterInstance(fighterName)));
            JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
        }
    }

    public static void CreateUserInstance(string userName, int wins = 0, int loses = 0, int elo = 0)
    {
        User user = User.Instance;
        user.SetUserValues(userName, wins, loses, elo);
    }

    public static Fighter CreateFighterInstance(string fighterName, float hp = 10, float damage = 1, float speed = 3, string species = "fire", int level = 1, int manaSlots = 10, List<Card> cards = null)
    {
        Fighter fighter = fighterGameObject.GetComponent<Fighter>();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, level, manaSlots, cards);
        return fighter;
    }
}