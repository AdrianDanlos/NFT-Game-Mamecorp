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
        buttonCloseCredits.GetComponent<Button>().onClick.AddListener(() => IHideCreditsPopup());
    }

    IEnumerator Start()
    {
        StartCoroutine(SceneManagerScript.instance.FadeIn());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        SceneFlag.sceneName = SceneNames.Credits.ToString();
    }

    public void IHideCreditsPopup()
    {
        StartCoroutine(HideCreditsPopup());
    }

    public IEnumerator HideCreditsPopup()
    {
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }
}
