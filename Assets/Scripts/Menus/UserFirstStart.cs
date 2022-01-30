using UnityEngine;
using Newtonsoft.Json.Linq;

public class UserFirstStart : MonoBehaviour
{
    void Start()
    {
        CreateUserFile();
        CreateFighterFile();
    }

    private static void CreateUserFile()
    {
        //On Login click
        //Get username from text field
        //replace it in the line below
        string userName = "userNameTypedByUser";
        UserFactory.CreateUserInstance(userName);
        JObject user = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
    }
    private static void CreateFighterFile()
    {
        //TODO: Pedir nombre al usuario en escena
        string fighterName = "fighterNameTypedByUser";
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreateFighterInstance(fighterName)));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
    }

}
