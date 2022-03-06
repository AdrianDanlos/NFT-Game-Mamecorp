using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Fighter player;
    public GameObject playerNameGO;
    public GameObject playerLevelGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    public GameObject gold;
    public GameObject gems;
    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    void Start()
    {
        player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);

        MenuUtils.SetName(playerNameGO);
        MenuUtils.SetLevelSlider(playerLevelGO, playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.SetElo(playerEloGO);
        
        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;
    }
}
