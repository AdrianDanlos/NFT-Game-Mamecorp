using UnityEngine;
using UnityEngine.UI;

public class OnClickCup : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClickHandler());
    }
    public void OnClickHandler()
    {
        // activate cup
        // handles modified version of combat
        Cup.Instance.isActive = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Cup.ToString());
    }
}
