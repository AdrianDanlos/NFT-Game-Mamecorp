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
        if (File.Exists(JsonDataManager.getFilePath("user")))
        {
            JObject userData = JsonDataManager.ReadData("user");
            CreateUserInstance((string)userData["userName"], (int)userData["wins"], (int)userData["loses"], (int)userData["elo"]);
        }
        else
        {
            //TODO: Pedir nombre al usuario en escena
            string userName = "userNameTypedByUser";
            CreateUserInstance(userName);
            JObject user = JObject.FromObject(User.Instance);
            JsonDataManager.SaveData(user, "user");
        };
    }

    public static void ReadOrCreateFighterFile()
    {
        if (File.Exists(JsonDataManager.getFilePath("fighter")))
        {
            JObject fighterData = JsonDataManager.ReadData("fighter");
            CreateFighterInstance((string)fighterData["fighterName"], (float)fighterData["hp"], (float)fighterData["damage"], (float)fighterData["speed"],
                (string)fighterData["species"], (int)fighterData["level"], (int)fighterData["manaSlots"], fighterData["cards"].ToObject<List<Card>>());
        }
        else
        {
            //TODO: Pedir nombre al usuario en escena
            string fighterName = "fighterNameTypedByUser";
            CreateFighterInstance(fighterName);
            JObject serializableFighter = JObject.FromObject(CreateSerializableFighterInstance(fighterName));
            JsonDataManager.SaveData(serializableFighter, "fighter");
        }

    }

    public static void CreateUserInstance(string userName, int wins = 0, int loses = 0, int elo = 0)
    {
        User user = User.Instance;
        user.SetUserValues(userName, wins, loses, elo);
    }

    //We need to create a fighter class that is not monobehaviour to be able to serialize and save the data into the JSON file.
    public static SerializableFighter CreateSerializableFighterInstance(string fighterName)
    {
        return new SerializableFighter(fighterName, 10, 1, 3, "Fire", 1, 10, new List<Card>());
    }


    //TODO: Do we need to return this fighter or can we get it from the scene at any time?
    public static void CreateFighterInstance(string fighterName, float hp = 10, float damage = 1, float speed = 3, string species = "fire", int level = 1, int manaSlots = 10, List<Card> cards = null)
    {
        //Can also be linked through the inspector instead of doing this
        Fighter fighter = fighterGameObject.AddComponent<Fighter>();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, level, manaSlots, cards);
    }
}