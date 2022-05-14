using UnityEngine;

public class OnClickGoToMainMenuOrCup : MonoBehaviour
{
    public void GoToMainMenu()
    {
        if (Cup.Instance.isActive && !CombatMode.isSoloqEnabled)
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Cup.ToString());
        if (CombatMode.isSoloqEnabled)
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }
}
