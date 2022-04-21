using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    void Start()
    {
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);
        MenuUtils.ShowElo(playerEloGO);
        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;
    }
}
