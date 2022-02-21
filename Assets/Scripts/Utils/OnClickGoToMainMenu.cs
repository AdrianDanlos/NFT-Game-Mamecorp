using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickGoToMainMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }
}
