using UnityEngine;

public class CanvasRaycastBlocker : MonoBehaviour
{
    private CanvasGroup[] canvasList;

    private void Awake()
    {
        CanvasGroup[] canvasList = FindObjectsOfType<CanvasGroup>();
        BlockRaycast();
    }

    private void Update()
    {
        if(SceneManagerScript.instance.hasFadingEnded)
            AllowRaycast();
    }

    private void BlockRaycast()
    {
        for(int i = 0; i < canvasList.Length; i++)
        {
            canvasList[i].interactable = false;
            canvasList[i].blocksRaycasts = true;
        }
    }

    private void AllowRaycast()
    {
        for (int i = 0; i < canvasList.Length; i++)
        {
            canvasList[i].interactable = true;
            canvasList[i].blocksRaycasts = false;
        }
    }
}
