using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DailyGift : MonoBehaviour
{
    // UI
    GameObject confirmGiftCanvas;
    GameObject giftCollectedButton;
    List<GameObject> giftItems = new List<GameObject>();
    TextMeshProUGUI timer;
    GameObject timerGO;

    // Manager
    MainMenu mainMenu;

    // variables
    string lastButtonClicked = "";

    private void Awake()
    {
        timer = GameObject.Find("Text_Daily_Time").GetComponent<TextMeshProUGUI>();
        timerGO = GameObject.Find("Icon_Daily_Time");
        confirmGiftCanvas = GameObject.Find("Canvas_Gift_Collected");
        giftCollectedButton = GameObject.Find("Button_BackToDailyGift");
        mainMenu = GameObject.Find("MainMenuManager").GetComponent<MainMenu>(); // notifications system

        // on enable
        timerGO.SetActive(false);
        confirmGiftCanvas.SetActive(false);
        GetDailyItems();
        giftCollectedButton.GetComponent<Button>().onClick.AddListener(() => GoToMainMenu());
        DisableInteraction();
        LoadUI();
        EnableNextReward();
    }

    private void Update()
    {
        if (IsGiftAvailable())
            EnableNextReward();
        if (IsOutOfRewards())
            ResetWeek();
        if (UpdateTimer() >= TimeSpan.Zero)
        {
            timerGO.SetActive(true);
            timer.text = UpdateTimer().ToString(@"hh\:mm\:ss");
        }
    }

    /* Day items structure
     * 
     * - 1-6 rewards GO container
     *      - DayX
     *          - VFX
     *          - UI
     *          - UI
     *          - Reward value
     *          - Reward collected UI
     *          - Focus UI (item is collectable)
     *              - GO container
     *              - Text
     * - 7 reward
     */

    private void LoadUI()
    {
        // 0 - false
        // 1 - true
        float flag;
        string day;

        for (int i = 0; i < giftItems.Count; i++)
        {
            day = "DAY" + (i + 1);
            flag = PlayerPrefs.GetFloat(day);

            if(flag == 1)
                DisableButtonOnRewardCollected(day);
        }
    }

    private void SaveDay(string day)
    {
        PlayerPrefs.SetFloat(day, 1);
        PlayerPrefs.Save();
    }

    private void GetDailyItems()
    {
        for(int i = 1; i <= 7; i++)
            giftItems.Add(GameObject.Find("Day" + i));
    }

    private void DisableInteraction()
    {
        for (int i = 0; i < giftItems.Count; i++)
            giftItems[i].GetComponent<Button>().interactable = false;
    }

    public void ResetWeek()
    {
        for(int i = 0; i < giftItems.Count; i++)
            PlayerPrefs.SetFloat("DAY" + (i + 1), 0);

        PlayerPrefs.Save();
    }

    private bool IsOutOfRewards()
    {
        float counter = 0;

        for (int i = 0; i < giftItems.Count; i++)
             counter += PlayerPrefs.GetFloat("DAY" + (i + 1), 0);

        return counter == 7;
    }

    private Dictionary<string, string> GetRewardType(string day)
    {
        day = day.ToUpper();

        return new Dictionary<string, string>
        {
            { 
              DailyGiftDB.gifts[(DailyGiftDB.Days)Enum.Parse(typeof(DailyGiftDB.Days), day)]["reward"],
              DailyGiftDB.gifts[(DailyGiftDB.Days)Enum.Parse(typeof(DailyGiftDB.Days), day)]["value"]
            }
        };
    }

    private void GiveReward(Dictionary<string, string> reward)
    {
        if (reward.ContainsKey("gold"))
            CurrencyHandler.instance.AddGold(int.Parse(reward["gold"]));
        if (reward.ContainsKey("gems"))
            CurrencyHandler.instance.AddGems(int.Parse(reward["gems"]));
        if (reward.ContainsKey("chest"))
            // give chest here

        // show confirm button + manage UI
        confirmGiftCanvas.SetActive(true);
    }

    public void GiveRewardButton()
    {
        if(PlayerPrefs.GetFloat("firstDailyGift") == 0)
            SaveFirstTime(1);
        lastButtonClicked = EventSystem.current.currentSelectedGameObject.name.ToUpper();
        GiveReward(GetRewardType(lastButtonClicked));
        DisableButtonOnRewardCollected(lastButtonClicked);
        SaveDay(lastButtonClicked);
        mainMenu.DisableDailyGiftNotification();
        StartCountdown();
    }

    private void DisableButtonOnRewardCollected(string day)
    {
        for(int i = 0; i < giftItems.Count; i++)
        {
            if(giftItems[i].name.ToUpper() == day)
            {
                giftItems[i].transform.GetChild(4).gameObject.SetActive(true);
                giftItems[i].transform.GetChild(5).gameObject.SetActive(false);
                giftItems[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void EnableNextReward()
    {
        for (int i = 0; i < giftItems.Count; i++)
        {
            if (!giftItems[i].transform.GetChild(4).gameObject.activeSelf && IsGiftAvailable())
            {
                giftItems[i].transform.GetChild(4).gameObject.SetActive(false);
                giftItems[i].transform.GetChild(5).gameObject.SetActive(true);
                giftItems[i].GetComponent<Button>().interactable = true;

                return;
            }
        }
    }

    private void StartCountdown()
    {
        PlayerPrefs.SetString("giftCountdown", DateTime.Now.AddDays(1).ToBinary().ToString());
        PlayerPrefs.Save();
    }

    public bool IsGiftAvailable()
    {
        if (PlayerPrefs.GetFloat("firstDailyGift") == 0)
            return true;

        if (PlayerPrefs.GetString("giftCountdown") != "")
            return DateTime.Compare(DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("giftCountdown"))), DateTime.Now) <= 0;

        return false;
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }

    public bool IsFirstTime()
    {
        return PlayerPrefs.GetFloat("firstDailyGift") == 0;
    }

    public void SaveFirstTime(int flag)
    {
        PlayerPrefs.SetFloat("firstDailyGift", flag);
        PlayerPrefs.Save();
    }

    private TimeSpan UpdateTimer()
    {
        if (PlayerPrefs.GetString("giftCountdown") != "")
            return DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("giftCountdown"))) - DateTime.Now;
        return TimeSpan.Zero;
    }
}
