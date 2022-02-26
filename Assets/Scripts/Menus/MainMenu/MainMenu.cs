using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Fighter player;
    int maxXp;
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
        MenuUtils.SetLevel(playerLevelGO, player.level);
        MenuUtils.SetExperiencePoints(playerNameGO, player.level, player.experiencePoints);
        MenuUtils.SetSlider(playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.SetGold(gold);
        MenuUtils.SetEnergy(energy);
    }
}
