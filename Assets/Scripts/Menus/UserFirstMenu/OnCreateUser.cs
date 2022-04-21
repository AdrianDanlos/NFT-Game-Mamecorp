using UnityEngine;
using Newtonsoft.Json.Linq;

public class OnCreateUser : MonoBehaviour
{
    // public GameObject nickNameInput;
    public void CreateUserFile()
    {
        //string userName = nickNameInput.GetComponent<TextMeshProUGUI>().text;
        string userName = "unknown"; //We dont have a userName concept anymore
        UserFactory.CreateUserInstance(userName, PlayerUtils.maxEnergy);
        JObject user = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
    }
}
