using System.IO;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    // UI 
    GameObject loadingBarGO;
    TextMeshProUGUI loadingText;
    Slider loadingBar;

    private void Awake()
    {
        loadingBarGO = GameObject.Find("Slider_LoadingBar");
        loadingText = loadingBarGO.GetComponentInChildren<TextMeshProUGUI>();
        loadingBar = loadingBarGO.GetComponent<Slider>();
    }

    public static GameObject fighterGameObject;
    IEnumerator Start()
    {
        HideFighter();

        //TODO: Set the time of loading screen
        StartCoroutine(FakeDelay());
        yield return new WaitForSeconds(3.5f);

        bool saveFilesFound = File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)) &&
            File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName));

        if (saveFilesFound)
        {
            JsonDataManager.ReadUserFile();
            JsonDataManager.ReadFighterFile();
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        }

        else UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.ChooseFirstFighter.ToString());
    }

    IEnumerator FakeDelay()
    {
        loadingText.text = "0%";
        loadingBar.value = 0f;
        yield return new WaitForSeconds(1f);

        loadingText.text = "30%";
        loadingBar.value = 0.3f;
        yield return new WaitForSeconds(1f);

        loadingText.text = "70%";
        loadingBar.value = 0.7f;
        yield return new WaitForSeconds(0.5f);

        loadingText.text = "100%";
        loadingBar.value = 1f;
        yield return new WaitForSeconds(1f);
    }

    private void HideFighter()
    {
        fighterGameObject = GameObject.Find("Fighter");
        fighterGameObject.SetActive(false);
    }
}