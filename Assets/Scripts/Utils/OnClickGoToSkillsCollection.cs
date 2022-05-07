using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickGoToSkillsCollection : MonoBehaviour
{
    public void GoToSkillsCollection()
    {
        Notifications.DecreaseInventory();
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Inventory.ToString());
    }
}
