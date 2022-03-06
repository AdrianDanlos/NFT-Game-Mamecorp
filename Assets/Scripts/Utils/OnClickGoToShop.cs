using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickGoToShop : MonoBehaviour
{
    public void GoToShop()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Shop.ToString());
    }
}




