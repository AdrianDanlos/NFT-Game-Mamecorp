using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickBattle : MonoBehaviour
{
    public void OnClickHandler()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Combat");
    }
}
