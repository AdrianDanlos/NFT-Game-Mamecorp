using System.Collections;
using UnityEngine;

public static class SceneManagerStatic
{
    public static readonly float fadeDuration = 1f;
    public static float alphaValue = 1f;

    public static IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        alphaValue = 0f;
        float fadeCounter = 0f;
        float fadeIncrement = 0.1f;

        do
        {
            canvasGroup.alpha += fadeIncrement;
            fadeCounter += fadeIncrement;
            yield return new WaitForSeconds(fadeIncrement);
        } while (fadeCounter < fadeDuration);
    }

    public static IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        alphaValue = 1f;
        float fadeCounter = 0f;
        float fadeIncrement = 0.1f;

        do
        {
            canvasGroup.alpha -= fadeIncrement;
            fadeCounter += fadeIncrement;
            yield return new WaitForSeconds(fadeIncrement);
        } while (fadeCounter < fadeDuration);
    }
}
