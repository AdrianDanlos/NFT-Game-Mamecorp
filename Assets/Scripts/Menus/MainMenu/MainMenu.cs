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
    public GameObject energy;
    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    void Start()
    {
        player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);

        MenuUtils.SetName(playerNameGO);
        MenuUtils.SetLevelSlider(playerLevelGO, playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        MenuUtils.SetElo(playerEloGO);

        SetEnergy();
    }

    private void SetEnergy()
    {
        //Move this logic to a reusable place
        //////////
        if (User.Instance.energy < PlayerUtils.maxEnergy
            && PlayerPrefs.HasKey("countdownEndTime")
            && EnergyManager.IsCountdownOver())
        {
            User.Instance.energy++;
            if (User.Instance.energy < PlayerUtils.maxEnergy) EnergyManager.StartCountdown();
        }
        ////////

        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;

        MenuUtils.SetEnergy(energy);
        //Set energy timer on main menu
    }
}
