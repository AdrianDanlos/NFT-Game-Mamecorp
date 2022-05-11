using UnityEngine;
using Newtonsoft.Json.Linq;

public class OnCreateUser : MonoBehaviour
{
    // public GameObject nickNameInput;
    public void CreateUserFile()
    {
        //string userName = nickNameInput.GetComponent<TextMeshProUGUI>().text;
        string userName = "unknown"; //We dont have a userName concept anymore
        string userIcon = GenerateIcon().ToString();
        UserFactory.CreateUserInstance(userName, userIcon, PlayerUtils.maxEnergy);
        JObject user = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
    }

    private int GenerateIcon()
    {
        return Random.Range(0, Resources.LoadAll<Sprite>("Icons/UserIcons/").Length) + 1;
    }
}
