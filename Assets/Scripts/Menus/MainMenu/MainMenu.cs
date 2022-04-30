using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // UI
    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    public GameObject notifyCards;
    public GameObject settings;

    // daily gift
    DailyGift dailyGift;
    GameObject dailyGiftsNotification;

    void Awake()
    {
        settings = GameObject.Find("Settings");
        dailyGift = GameObject.Find("DailyRewards").GetComponent<DailyGift>();
        dailyGiftsNotification = GameObject.Find("DailyGiftsNotification");

        Fighter player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);
        MenuUtils.ShowElo(playerEloGO);
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("Levels"));
        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;

        // Notifications
        if (Notifications.isInventoryNotificationsOn)
            notifyCards.SetActive(true);

        // on open
        settings.SetActive(false);
        dailyGiftsNotification.SetActive(false);
    }

    IEnumerator Start()
    {
        //If the user don't have any energy left check each its energy each second to activate the battle button once an energy point is given.
        while (User.Instance.energy == 0) yield return new WaitForSeconds(1f);
        battleButtonGO.GetComponent<Button>().interactable = true;
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void EnableDailyGiftNotification()
    {
        dailyGiftsNotification.SetActive(true);
        dailyGiftsNotification.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "1";
    }

    public void DisableDailyGiftNotification()
    {
        dailyGiftsNotification.SetActive(false);
    }
}
