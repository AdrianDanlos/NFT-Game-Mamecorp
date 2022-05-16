using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CupTimer : MonoBehaviour
{
    // UI
    GameObject buttonCup;
    TextMeshProUGUI textCup;
    TextMeshProUGUI textCupDisabled;
    GameObject timerGO;
    TextMeshProUGUI textTimer;
    Image lockIcon;

    private void Awake()
    {
        SetupUI();
        SetupCup();
    }

    private void SetupUI()
    {
        buttonCup = GameObject.Find("Button_Cup");
        textCup = GameObject.Find("Text_Cup").GetComponent<TextMeshProUGUI>();
        textCupDisabled = GameObject.Find("Text_Cup_Disabled").GetComponent<TextMeshProUGUI>();
        timerGO = GameObject.Find("Icon_Daily_Time");
        textTimer = GameObject.Find("Text_Daily_Time").GetComponent<TextMeshProUGUI>();
        lockIcon = GameObject.Find("Cup_Lock").GetComponent<Image>();
    }

    private void Update()
    {
        SetupCup();
    }

    private void SetupCup()
    {
        if (IsCupAvailable())
            EnableCup();
        if (UpdateTimer() >= TimeSpan.Zero)
        {
            DisableCup();
            timerGO.SetActive(true);
            textTimer.text = UpdateTimer().ToString(@"hh\:mm\:ss");
        }
    }

    private void EnableCup()
    {
        buttonCup.GetComponent<Button>().enabled = true;
        lockIcon.enabled = false;
        textCup.enabled = true;
        textCupDisabled.enabled = false;
        timerGO.SetActive(false);
    }

    private void DisableCup()
    {
        buttonCup.GetComponent<Button>().enabled = false;
        lockIcon.enabled = true;
        textCup.enabled = false;
        textCupDisabled.enabled = true;
        timerGO.SetActive(true);
    }

    public bool IsCupAvailable()
    {
        if (PlayerPrefs.GetFloat("firstCup") == 0)
        {
            PlayerPrefs.SetFloat("firstCup", 1);
            PlayerPrefs.Save();
            StartCountdown();
            return false;
        }

        if (PlayerPrefs.GetString("cupCountdown") != "")
            return DateTime.Compare(DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("cupCountdown"))), DateTime.Now) <= 0;

        return false;
    }

    private void StartCountdown()
    {
        // TODO change to real date
        PlayerPrefs.SetString("cupCountdown", DateTime.Now.AddSeconds(20).ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private TimeSpan UpdateTimer()
    {
        if (PlayerPrefs.GetString("cupCountdown") != "")
            return DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("cupCountdown"))) - DateTime.Now;
        return TimeSpan.Zero;
    }
}
