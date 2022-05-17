using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CreditsManager : MonoBehaviour
{
    // UI
    GameObject buttonCloseCredits;

    private void Awake()
    {
        buttonCloseCredits = GameObject.Find("Button_Close_Credits");
        buttonCloseCredits.GetComponent<Button>().onClick.AddListener(() => HideCreditsPopup());
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        StartCoroutine(SceneManagerScript.instance.FadeIn());

        SceneFlag.sceneName = SceneNames.Credits.ToString();
    }

    public void IHideCreditsPopup()
    {
        StartCoroutine(HideCreditsPopup());
    }

    public IEnumerator HideCreditsPopup()
    {
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }
}
