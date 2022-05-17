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

    // animations
    Animator titleAnimator;
    Animator dev1Animator;
    Animator dev2Animator;
    Animator dev3Animator;
    Animator copyAnimator;

    private void Awake()
    {
        SetupUI();
        SetupButtons();
    }

    IEnumerator Start()
    {
        StartCoroutine(SceneManagerScript.instance.FadeIn());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        SceneFlag.sceneName = SceneNames.Credits.ToString();

        IStartAnimation();
    }

    private void SetupButtons()
    {
        buttonCloseCredits.GetComponent<Button>().onClick.AddListener(() => IHideCreditsPopup());
    }

    private void SetupUI()
    {
        buttonCloseCredits = GameObject.Find("Button_Close_Credits");
        title = GameObject.Find("Title").GetComponent<TextMeshProUGUI>();
        dev1 = GameObject.Find("Dev1").GetComponent<TextMeshProUGUI>();
        dev2 = GameObject.Find("Dev2").GetComponent<TextMeshProUGUI>();
        dev3 = GameObject.Find("Dev3").GetComponent<TextMeshProUGUI>();
        copy = GameObject.Find("Copy").GetComponent<TextMeshProUGUI>();

        titleAnimator = title.gameObject.GetComponent<Animator>();
        dev1Animator = dev1.gameObject.GetComponent<Animator>();
        dev2Animator = dev2.gameObject.GetComponent<Animator>();
        dev3Animator = dev3.gameObject.GetComponent<Animator>();
        copyAnimator = copy.gameObject.GetComponent<Animator>();

        title.enabled = false;
        dev1.enabled = false;
        dev2.enabled = false;
        dev3.enabled = false;
        copy.enabled = false;

        titleAnimator.enabled = false;
        dev1Animator.enabled = false;
        dev2Animator.enabled = false;
        dev3Animator.enabled = false;
        copyAnimator.enabled = false;
    }

    private void IStartAnimation()
    {
        StartCoroutine(StartAnimation());
    }

    public IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        titleAnimator.enabled = true;
        copyAnimator.enabled = true;
        title.enabled = true;
        copy.enabled = true;

        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));

        dev1Animator.enabled = true;
        dev1.enabled = true;

        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(4f));

        dev2Animator.enabled = true;
        dev2.enabled = true;

        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(4f));

        dev3Animator.enabled = true;
        dev3.enabled = true;
    }

    public void IHideCreditsPopup()
    {
        StartCoroutine(HideCreditsPopup());
    }

    private IEnumerator HideCreditsPopup()
    {
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }
}
