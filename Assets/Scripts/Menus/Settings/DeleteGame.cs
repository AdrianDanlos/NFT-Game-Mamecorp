using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadScene(SceneNames.EntryPoint.ToString());
    }

    private void DeleteSaves()
    {
        string[] files =  Directory.GetFiles(Application.persistentDataPath);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }
    }

    private void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
