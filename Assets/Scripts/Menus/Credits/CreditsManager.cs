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
    TextMeshProUGUI title;
    TextMeshProUGUI dev1;
    TextMeshProUGUI dev2;
    TextMeshProUGUI dev3;
    TextMeshProUGUI copy;

    private void Awake()
    {
        buttonCloseCredits = GameObject.Find("Button_Close_Credits");
        title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        dev1 = GameObject.Find("Dev1").GetComponent<TextMeshProUGUI>();
        dev2 = GameObject.Find("Dev2").GetComponent<TextMeshProUGUI>();
        dev3 = GameObject.Find("Dev3").GetComponent<TextMeshProUGUI>();
        copy = GameObject.Find("Copy").GetComponent<TextMeshProUGUI>();

        SetupUI();

        buttonCloseCredits.GetComponent<Button>().onClick.AddListener(() => IHideCreditsPopup());
    }

    IEnumerator Start()
    {
        StartCoroutine(SceneManagerScript.instance.FadeIn());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        StartCoroutine(StartAnimation());

        SceneFlag.sceneName = SceneNames.Credits.ToString();
    }

    private void SetupUI()
    {
        title.enabled = false;
        dev1.enabled = false;
        dev2.enabled = false;
        dev3.enabled = false;
        copy.enabled = false;
    }

    IEnumerator StartAnimation()
    {
        IFadeIn(title);
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        IFadeOut(title);
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
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

    private void IFadeOut(TextMeshProUGUI text)
    {
        StartCoroutine(FadeOut(text));
    }

    private void IFadeIn(TextMeshProUGUI text)
    {
        StartCoroutine(FadeIn(text));
    }

    private IEnumerator FadeOut(TextMeshProUGUI text)
    {
        byte alphaValue = 0;
        text.enabled = true;
        text.color = new Color32(255, 255, 255, alphaValue);
        float fadeIncrement = 0.1f;

        do
        {
            text.color = new Color32(255, 255, 255, alphaValue);
            alphaValue += 26;
            yield return new WaitForSeconds(fadeIncrement);
        } while (alphaValue <= 255);
    }

    private IEnumerator FadeIn(TextMeshProUGUI text)
    {
        byte alphaValue = 255;
        text.enabled = true;
        text.color = new Color32(255, 255, 255, alphaValue);
        float fadeIncrement = 0.1f;

        do
        {
            text.color = new Color32(255, 255, 255, alphaValue);
            alphaValue -= 26;
            yield return new WaitForSeconds(fadeIncrement);
        } while (alphaValue >= 0);
    }
}
