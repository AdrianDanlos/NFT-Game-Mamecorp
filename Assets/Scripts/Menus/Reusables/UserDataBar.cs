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

        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        EnergyManager.RefreshEnergyBasedOnCountdown();
        MenuUtils.SetEnergy(energy);
        MenuUtils.DisplayEnergyCountdown(timerContainer, timer);
        MenuUtils.SetName(playerNameGO, player.fighterName);
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("Levels"));    
    }
}