using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using TMPro;

public class OnCreateUser : MonoBehaviour
{
    public GameObject nickNameInput;
    public void onClickContinue()
    {
        CreateUserFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.ChooseFirstFighter.ToString());

    }
    private void CreateUserFile()
    {
        string userName = nickNameInput.GetComponent<TextMeshProUGUI>().text;
        UserFactory.CreateUserInstance(userName, PlayerUtils.maxEnergy);
        JObject user = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
    }
}
