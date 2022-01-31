using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using TMPro;

public class OnClickContinue : MonoBehaviour
{
    public GameObject nickNameInput;
    public void onClickHandler()
    {
        CreateUserFile();
        CreateFighterFile();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Combat");
    }

    private void CreateUserFile()
    {
        string userName = nickNameInput.GetComponent<TextMeshProUGUI>().text;
        UserFactory.CreateUserInstance(userName);
        JObject user = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
    }
    private void CreateFighterFile()
    {
        //TODO: Pedir nombre al usuario en escena
        string fighterName = "fighterNameTypedByUser";
        JObject serializableFighter = JObject.FromObject(JsonDataManager.CreateSerializableFighterInstance(FighterFactory.CreateFighterInstance(fighterName)));
        JsonDataManager.SaveData(serializableFighter, JsonDataManager.FighterFileName);
    }
}
