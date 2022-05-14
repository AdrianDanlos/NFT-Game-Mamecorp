using UnityEngine;
using Newtonsoft.Json.Linq;

public class OnCreateUser : MonoBehaviour
{
    public void CreateUserFile()
    {
        // TODO add flag to user
        string userIcon = GenerateIcon().ToString();
        UserFactory.CreateUserInstance(userIcon, PlayerUtils.maxEnergy);
        JObject user = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
    }

    private int GenerateIcon()
    {
        return Random.Range(0, Resources.LoadAll<Sprite>("Icons/UserIcons/").Length) + 1;
    }
}
