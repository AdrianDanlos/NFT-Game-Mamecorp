using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // UI
    public GameObject playerEloGO;
    public GameObject battleButtonGO;
    public GameObject cardsButtonGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSlider;
    public GameObject notifyCards;
    public GameObject settings;

    // stats
    public TextMeshProUGUI attack;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI speed;

    // daily gift
    GameObject dailyGiftCanvas;
    DailyGift dailyGift;
    GameObject dailyGiftsNotification;

    // ranking 
    GameObject rankingCanvas;

    // profile
    GameObject profileCanvas;

    void Awake()
    {
        settings = GameObject.Find("Settings");
        dailyGiftCanvas = GameObject.Find("DailyRewardsCanvas");
        rankingCanvas = GameObject.Find("RankingCanvas");
        profileCanvas = GameObject.Find("ProfileCanvas");
        dailyGift = dailyGiftCanvas.GetComponent<DailyGift>();
        dailyGiftsNotification = GameObject.Find("DailyGiftsNotification");

        // stats
        attack = GameObject.Find("Attack_Value").GetComponent<TextMeshProUGUI>();
        hp = GameObject.Find("Hp_Value").GetComponent<TextMeshProUGUI>();
        speed = GameObject.Find("Speed_Value").GetComponent<TextMeshProUGUI>();

        Fighter player = PlayerUtils.FindInactiveFighter();
        PlayerUtils.FindInactiveFighterGameObject().SetActive(false);
        MenuUtils.ShowElo(playerEloGO);
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("Levels"));
        MenuUtils.SetFighterStats(attack, hp, speed);
        battleButtonGO.GetComponent<Button>().interactable = User.Instance.energy > 0;
        cardsButtonGO.GetComponent<Button>().interactable = player.skills.Count > 0;

        // Notifications
        if (Notifications.isInventoryNotificationsOn)
            notifyCards.SetActive(true);

        // Hide poppups
        settings.SetActive(false);
        dailyGiftCanvas.SetActive(false);
        rankingCanvas.SetActive(false);
        profileCanvas.SetActive(false);
        dailyGiftsNotification.SetActive(false);
        if (dailyGift.IsFirstTime())
            dailyGiftsNotification.SetActive(true);
    }

    IEnumerator Start()
    {
        //If the user don't have any energy left check each its energy each second to activate the battle button once an energy point is given.
        while (User.Instance.energy == 0) yield return new WaitForSeconds(1f);
        battleButtonGO.GetComponent<Button>().interactable = true;
    }

    private void Update()
    {
        if (dailyGift.IsGiftAvailable() || dailyGift.IsFirstTime())
            dailyGiftsNotification.SetActive(true);
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
