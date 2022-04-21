using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class MenuUtils
{
    public static void SetName(GameObject playerNameGO, string fighterName)
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = fighterName;
    }
    public static void SetLevelSlider(GameObject playerExpGO, GameObject playerLevelSliderGO, int playerLevel, int playerExp)
    {
        int maxExp = Levels.MaxXpOfCurrentLevel(playerLevel);
        playerExpGO.GetComponent<TextMeshProUGUI>().text = $"{playerExp.ToString()}/{maxExp}";
        playerLevelSliderGO.GetComponent<Slider>().value = (float)playerExp / (float)maxExp;
    }
    public static void SetGold(GameObject goldGO)
    {
        goldGO.GetComponent<TextMeshProUGUI>().text = User.Instance.gold.ToString();
    }

    public static void SetGems(GameObject gemsGO)
    {
        gemsGO.GetComponent<TextMeshProUGUI>().text = User.Instance.gems.ToString();
    }
    public static void ShowElo(GameObject playerEloGO)
    {
        playerEloGO.GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();
    }

    public static void SetEnergy(GameObject energyGO)
    {
        energyGO.GetComponent<TextMeshProUGUI>().text = $"{User.Instance.energy.ToString()}/{PlayerUtils.maxEnergy}";
    }
    public static void DisplayEnergyCountdown(GameObject timerContainerGO, GameObject timerGO)
    {
        if (!EnergyManager.UserHasMaxEnergy())
        {
            var timeSinceCountdownStart = EnergyManager.GetTimeSinceCountdownStart();
            int minutesPassedOfCurrentCountdown = Math.Abs(timeSinceCountdownStart.Minutes % EnergyManager.defaultEnergyRefreshTimeInMinutes);
            string minutesUntilCountdownEnd = (EnergyManager.defaultEnergyRefreshTimeInMinutes - minutesPassedOfCurrentCountdown - 1).ToString();
            string secondsUntilCountdownEnd = (60 - timeSinceCountdownStart.Seconds).ToString();

            timerContainerGO.SetActive(true);
            timerGO.GetComponent<TextMeshProUGUI>().text = $"{minutesUntilCountdownEnd}m {secondsUntilCountdownEnd}s";
            return;
        }

        timerContainerGO.SetActive(false);
    }

    public static void SetLevelIcon(int playerLevel)
    {
        float levelsUntilIconUpgrade = 3;
        double nIcon = Mathf.Ceil(playerLevel / levelsUntilIconUpgrade);
        Image iconGO = GameObject.Find($"Level_Icon_{nIcon}").GetComponent<Image>();
        iconGO.enabled = true;
        iconGO.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = playerLevel.ToString();

        //hide previous icon if any
        if (GameObject.Find($"Level_Icon_{nIcon - 1}")) GameObject.Find($"Level_Icon_{nIcon - 1}").SetActive(false);
    }
}