using System.Collections;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    // instance
    public static SceneManagerScript instance;

    private CanvasGroup fadeCanvasGroup;
    public const float FADE_DURATION = 1f;
    public const float FADE_INCREMENT = 0.02f;
    public const float ANIMATION_SPEED = 2f;
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
        float fadeIncrement = FADE_INCREMENT;

        do
        {
            fadeCanvasGroup.alpha += fadeIncrement * ANIMATION_SPEED;
            alphaValue += fadeIncrement * ANIMATION_SPEED;
            fadeCounter += fadeIncrement * ANIMATION_SPEED;
            yield return new WaitForSeconds(fadeIncrement);
        } while (fadeCounter < FADE_DURATION);
    }

    public IEnumerator FadeIn()
    {
        alphaValue = 1f;
        float fadeCounter = 0;
        float fadeIncrement = FADE_INCREMENT;

        do
        {
            fadeCanvasGroup.alpha -= fadeIncrement * ANIMATION_SPEED;
            alphaValue -= fadeIncrement * ANIMATION_SPEED; 
            fadeCounter += fadeIncrement * ANIMATION_SPEED; 
            yield return new WaitForSeconds(fadeIncrement);
        } while (fadeCounter < FADE_DURATION);
    }
}
