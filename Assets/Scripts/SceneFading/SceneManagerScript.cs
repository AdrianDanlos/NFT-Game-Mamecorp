using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    // instance
    public static SceneManagerScript instance;

    private Canvas fadeCanvas;
    private CanvasGroup fadeCanvasGroup;

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
        fadeCanvas = GameObject.Find("FadeCanvas").GetComponent<Canvas>();
        fadeCanvasGroup = GameObject.Find("FadeCanvas").GetComponent<CanvasGroup>();
    }
}
