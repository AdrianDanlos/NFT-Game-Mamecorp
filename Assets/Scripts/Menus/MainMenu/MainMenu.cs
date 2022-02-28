using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Fighter player;
    public GameObject playerNameGO;
    public GameObject playerLevelGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    public GameObject gold;
    public GameObject energy;
    void Start()
    {
        player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);

        MenuUtils.SetName(playerNameGO);
        MenuUtils.SetLevelSlider(playerLevelGO, playerNameGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.SetGold(gold);
        MenuUtils.SetEnergy(energy);
    }
}
