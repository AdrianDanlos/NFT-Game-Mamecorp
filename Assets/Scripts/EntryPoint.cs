using System.IO;
using UnityEngine;
using Newtonsoft.Json.Linq;


public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        ApplicationStart();
    }

    public static void ApplicationStart()
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
        }
        
        //TODO: Do the same for fighter
    }

    public static User CreateUserInstance(string userName = "Berthold", int wins = 0, int loses = 0, int elo = 0)
    {
        User user = User.Instance;
        user.SetUserValues(userName, wins, loses, elo);
        return user;
    }

    public static Fighter CreateFighter()
    {
        //TODO: Pedir nombre del fighter al usuario en escena
        return null;
    }
}