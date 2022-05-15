using System.Collections;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    // instance
    public static SceneManagerScript instance;

    private CanvasGroup fadeCanvasGroup;
    public readonly float fadeDuration = 1f;
    public float alphaValue = 1f;

    private void Awake()
    {
        // instance
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // components
        fadeCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeOut()
    {
        alphaValue = 0f;
        float fadeCounter = 0f;
        float fadeIncrement = 0.02f;

        do
        {
            fadeCanvasGroup.alpha += fadeIncrement;
            alphaValue += fadeIncrement;
            fadeCounter += fadeIncrement;
            yield return new WaitForSeconds(fadeIncrement);
        } while (fadeCounter < fadeDuration);
    }

    public IEnumerator FadeIn()
    {
        alphaValue = 1f;
        float fadeCounter = 0f;
        float fadeIncrement = 0.02f;

        do
        {
            fadeCanvasGroup.alpha -= fadeIncrement;
            alphaValue -= fadeIncrement;
            fadeCounter += fadeIncrement;
            yield return new WaitForSeconds(fadeIncrement);
        } while (fadeCounter < fadeDuration);
    }
}
