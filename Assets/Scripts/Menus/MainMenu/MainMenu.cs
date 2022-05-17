using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject deleteConfirmation;
    public GameObject aboutPopup;
    public GameObject buttonDelete;
    public GameObject buttonAbout;
    public GameObject buttonCredits;
    public GameObject buttonCloseConfirmation;
    public GameObject buttonCloseAbout;

    // stats
    public TextMeshProUGUI attack;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI speed;

    // notifications
    public TextMeshProUGUI notifyCardsTxt;

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
        // TODO
        // FindObjectOfType<AudioManager>().Play("Theme");
        // FindObjectOfType<AudioManager>().PlayClipAtPoint("Test", transform.position);

        settings = GameObject.Find("Settings");
        deleteConfirmation = GameObject.Find("Delete_Confirmation");
        aboutPopup = GameObject.Find("AboutPopup");
        dailyGiftCanvas = GameObject.Find("DailyRewardsCanvas");
        rankingCanvas = GameObject.Find("RankingCanvas");
        profileCanvas = GameObject.Find("ProfileCanvas");
        dailyGift = dailyGiftCanvas.GetComponent<DailyGift>();
        dailyGiftsNotification = GameObject.Find("DailyGiftsNotification");
        buttonDelete = GameObject.Find("Button_Delete");
        buttonAbout = GameObject.Find("Button_About");
        buttonCredits = GameObject.Find("Button_Credits");
        buttonCloseConfirmation = GameObject.Find("Button_Close_Confirmation");
        buttonCloseAbout = GameObject.Find("Button_Close_About");

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
        notifyCardsTxt = notifyCards.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        // Hide poppups
        settings.SetActive(false);
        deleteConfirmation.SetActive(false);
        aboutPopup.SetActive(false);
        dailyGiftCanvas.SetActive(false);
        rankingCanvas.SetActive(false);
        profileCanvas.SetActive(false);
        dailyGiftsNotification.SetActive(false);
        if (dailyGift.IsFirstTime())
            dailyGiftsNotification.SetActive(true);

        buttonDelete.GetComponent<Button>().onClick.AddListener(() => ShowDeleteConfirmation());
        buttonCloseConfirmation.GetComponent<Button>().onClick.AddListener(() => CloseSettingsConfirmation());
        buttonAbout.GetComponent<Button>().onClick.AddListener(() => ShowAboutPopup());
        buttonCloseAbout.GetComponent<Button>().onClick.AddListener(() => HideAboutPopup());
        buttonCredits.GetComponent<Button>().onClick.AddListener(() => IShowCredits());
    }

    IEnumerator Start()
    {
        if (SceneFlag.sceneName == SceneNames.EntryPoint.ToString() ||
            SceneFlag.sceneName == SceneNames.Combat.ToString() ||
            SceneFlag.sceneName == SceneNames.LevelUp.ToString() ||
                SceneFlag.sceneName == SceneNames.Credits.ToString())
        {
            StartCoroutine(SceneManagerScript.instance.FadeIn());
            yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        }

        //If the user don't have any energy left check each its energy each second to activate the battle button once an energy point is given.
        while (User.Instance.energy == 0) yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        battleButtonGO.GetComponent<Button>().interactable = true;

        StartCoroutine(RefreshItems());

        SceneFlag.sceneName = SceneNames.MainMenu.ToString();
    }

    IEnumerator RefreshItems()
    {
        do
        {
            if (dailyGift.IsGiftAvailable() || dailyGift.IsFirstTime())
                dailyGiftsNotification.SetActive(true);

            // Notifications
            if (Notifications.isInventoryNotificationsOn)
            {
                notifyCards.SetActive(true);
                notifyCardsTxt.text = Notifications.cardsUnseen.ToString();
            }
            else
                notifyCards.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        while (true);
    }

    // on settings button
    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void ShowDeleteConfirmation()
    {
        deleteConfirmation.SetActive(true);
    }

    public void ShowAboutPopup()
    {
        aboutPopup.SetActive(true);
    }

    public void HideAboutPopup()
    {
        aboutPopup.SetActive(false);
    }

    public void IShowCredits()
    {
        StartCoroutine(ShowCredits());
    }

    public IEnumerator ShowCredits()
    {
        StartCoroutine(SceneManagerScript.instance.FadeOut());
        yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
        SceneManager.LoadScene(SceneNames.Credits.ToString());
    }

    public void CloseSettingsConfirmation()
    {
        deleteConfirmation.SetActive(false);
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
