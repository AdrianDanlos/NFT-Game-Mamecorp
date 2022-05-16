using System.Collections;
using UnityEngine;

public class OnClickGoToMainMenuOrCup : MonoBehaviour
{
    public void GoToMainMenu()
    {
        if (Cup.Instance.isActive && !CombatMode.isSoloqEnabled)
            IGoToScene(SceneNames.Cup);
        if (CombatMode.isSoloqEnabled)
            IGoToScene(SceneNames.MainMenu);
    }

    private IEnumerator GoToScene(SceneNames sceneName)
    {
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName.ToString());
    }

    private void IGoToScene(SceneNames sceneName)
    {
        StartCoroutine(GoToScene(sceneName));
    }
}
