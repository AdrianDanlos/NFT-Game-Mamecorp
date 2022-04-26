using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    void Start()
    {
        Fighter player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);
        MenuUtils.ShowElo(playerEloGO);
        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("Levels"));
    }
}
