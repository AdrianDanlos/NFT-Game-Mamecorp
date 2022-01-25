using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        //FIXME: In the future recheck if its possible to group them under a single function e.g. ReadOrCreateSaveFiles that contains a loop.
        ReadOrCreateUserFile();
        ReadOrCreateFighterFile();
    }

    public static void ReadOrCreateUserFile()
    {
        if (File.Exists(JsonDataManager.getFilePath("user")))
        {
            var userData = JsonDataManager.ReadData("user");
            CreateUserInstance((string)userData["userName"], (int)userData["wins"], (int)userData["loses"], (int)userData["elo"]);
            Debug.Log(User.Instance.userName);
        }
        else
        {
            //TODO: Pedir nombre al usuario en escena
            JObject user = JObject.FromObject(CreateUserInstance());
            JsonDataManager.SaveData(user, "user");
        };
    }

    public static void ReadOrCreateFighterFile()
    {
        if (File.Exists(JsonDataManager.getFilePath("fighter")))
        {
            var fighterData = JsonDataManager.ReadData("fighter");
            Fighter fighter = CreateFighterInstance();
            Debug.Log(fighter.fighterName);
        }
        else
        {
            JObject fighter = JObject.FromObject(CreateSerializableFighterInstance());
            JsonDataManager.SaveData(fighter, "fighter");
        }
    }

    public static User CreateUserInstance(string userName = "UserNameX", int wins = 0, int loses = 0, int elo = 0)
    {
        User user = User.Instance;
        user.SetUserValues(userName, wins, loses, elo);
        return user;
    }

    //Explain why we need this
    public static SerializableFighter CreateSerializableFighterInstance()
    {
        //TODO: Pedir nombre al usuario en escena
        return new SerializableFighter("FighterNameX", 10, 1, 3, "Fire", 1, 10, new List<Card>());
    }

    public static Fighter CreateFighterInstance()
    {
        //Can also be linked through the inspector instead of doing this
        Fighter fighter = GameObject.Find("Fighter").AddComponent<Fighter>();
        fighter.FighterConstructor("FighterNameX", 10, 1, 3, "Fire", 1, 10, new List<Card>());
        return fighter;
    }
}