using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // UI
    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    public GameObject notifyCards;
    public GameObject settings;

    void Awake()
    {
        settings = GameObject.Find("Settings");

        Fighter player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);
        MenuUtils.ShowElo(playerEloGO);
        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("Levels"));

        // Notifications
        if (Notifications.isInventoryNotificationsOn)
            notifyCards.SetActive(true);

        // on open
        settings.SetActive(false);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }
}
