using System.Collections;
using UnityEngine;

public class OnClickGoToMainMenuOrCup : MonoBehaviour
{
    public void GoToMainMenu()
    {
        if (Cup.Instance.isActive && !CombatMode.isSoloqEnabled)
            IGoToCombat(SceneNames.Cup);
        if (CombatMode.isSoloqEnabled)
            IGoToCombat(SceneNames.MainMenu);
    }

    private IEnumerator GoToCombat(SceneNames sceneName)
    {
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName.ToString());
    }

    private void IGoToCombat(SceneNames sceneName)
    {
        StartCoroutine(GoToCombat(sceneName));
    }
}
