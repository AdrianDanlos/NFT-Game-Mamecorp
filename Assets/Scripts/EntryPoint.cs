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
    TextMeshProUGUI tipText;

    // fighter
    public static GameObject fighterGameObject;

    private void Awake()
    {
        loadingBarGO = GameObject.Find("Slider_LoadingBar");
        loadingText = loadingBarGO.GetComponentInChildren<TextMeshProUGUI>();
        loadingBar = loadingBarGO.GetComponent<Slider>();
        tipText = GameObject.Find("TipText").GetComponentInChildren<TextMeshProUGUI>();

        ResetBar();
    }

    IEnumerator Start()
    {
        HideFighter();
        GenerateTip();

        StartCoroutine(SceneManagerScript.instance.FadeIn());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(SceneFlag.FADE_DURATION));

        // --- Enable this for loading effect ---
        StartCoroutine(FakeDelay());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(3.5f));

        bool saveFilesFound = File.Exists(JsonDataManager.getFilePath(JsonDataManager.UserFileName)) &&
            File.Exists(JsonDataManager.getFilePath(JsonDataManager.FighterFileName));

        if (saveFilesFound)
        {
            JsonDataManager.ReadUserFile();
            JsonDataManager.ReadFighterFile();

            StartCoroutine(SceneManagerScript.instance.FadeOut());
            yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(SceneFlag.FADE_DURATION));
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
        }

        else
        {
            StartCoroutine(SceneManagerScript.instance.FadeOut());
            yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(SceneFlag.FADE_DURATION));
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.ChooseFirstFighter.ToString());
        }

        Notifications.InitiateCardsUnseen();
        SceneFlag.sceneName = SceneNames.EntryPoint.ToString();
    }

    private void ResetBar()
    {
        // set up bar
        loadingText.text = "0%";
        loadingBar.value = 0f;
    }

    IEnumerator FakeDelay()
    {
        loadingText.text = "0%";
        loadingBar.value = 0f;
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        loadingText.text = "30%";
        loadingBar.value = 0.3f;
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        loadingText.text = "70%";
        loadingBar.value = 0.7f;
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(0.5f));

        loadingText.text = "100%";
        loadingBar.value = 1f;
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(0.25f));
    }

    private void HideFighter()
    {
        fighterGameObject = GameObject.Find("Fighter");
        fighterGameObject.SetActive(false);
    }

    private void GenerateTip()
    {
        tipText.text = Tips.tips[Random.Range(0, Tips.tips.Count)];
    }
}