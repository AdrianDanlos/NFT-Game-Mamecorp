using UnityEngine;
using TMPro;

public class UserDataBar : MonoBehaviour
{
    public GameObject gold;
    public GameObject gems;
    public GameObject energy;
    public GameObject timerContainer;
    public GameObject timer;
    public GameObject playerNameGO;
    public GameObject playerLevelGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;

    void Start()
    {
        Fighter player = PlayerUtils.FindInactiveFighter();
        GameObject levelIcons = GameObject.Find("Levels");
        bool showLevel = levelIcons != null;

        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        EnergyManager.RefreshEnergyBasedOnCountdown();
        MenuUtils.SetEnergy(energy);
        MenuUtils.DisplayEnergyCountdown(timerContainer, timer);
        MenuUtils.SetName(playerNameGO, player.fighterName);

        //FIXME: This should be outside of the prefab. It is only called on the main menu
        if (showLevel)
        {
            MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
            MenuUtils.DisplayLevelIcon(player.level, levelIcons);
        }
    }
}