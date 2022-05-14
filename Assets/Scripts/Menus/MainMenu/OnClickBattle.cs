using UnityEngine;

public class OnClickBattle : MonoBehaviour
{
    public void OnClickHandler()
    {
        if(gameObject.name.Contains("Battle"))
            CombatMode.isSoloqEnabled = true;
        if(gameObject.name.Contains("Cup"))
            CombatMode.isSoloqEnabled = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Combat.ToString());
    }
}
