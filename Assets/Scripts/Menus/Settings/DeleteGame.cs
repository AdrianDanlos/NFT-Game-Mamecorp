using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DeleteGame : MonoBehaviour
{
    // UI
    GameObject resetGame; 

    private void Awake()
    {
        resetGame = GameObject.Find("Button_Delete_Confirmation");
        resetGame.GetComponent<Button>().onClick.AddListener(() => RestartGame());
    }

    public void RestartGame()
    {
        DeleteSaves();
        ResetAllPrefs();
        IGoToEntryPoint();
    }

    private void DeleteSaves()
    {
        string[] files =  Directory.GetFiles(Application.persistentDataPath);

        for (int i = 0; i < files.Length; i++)
            File.Delete(files[i]);
    }

    private void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator GoToEntryPoint()
    {
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(SceneFlag.FADE_DURATION));
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.EntryPoint.ToString());
    }

    private void IGoToEntryPoint()
    {
        StartCoroutine(GoToEntryPoint());
    }
}
