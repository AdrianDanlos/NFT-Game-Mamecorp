using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MenuUtils
{
    public static void SetName(GameObject playerNameGO)
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = User.Instance.userName;
    }
    public static void SetLevelSlider(GameObject playerLevelGO, GameObject playerExpGO, GameObject playerLevelSliderGO, int playerLevel, int playerExp)
    {
        int maxExp = Levels.MaxXpOfCurrentLevel(playerLevel);
        playerLevelGO.GetComponent<TextMeshProUGUI>().text = playerLevel.ToString();
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
    public static void SetElo(GameObject playerEloGO)
    {
        playerEloGO.GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();
    }

    public static void SetEnergy(GameObject energyGO)
    {
        energyGO.GetComponent<TextMeshProUGUI>().text = $"{User.Instance.energy.ToString()}/{PlayerUtils.maxEnergy}";
    }
    public static void DisplayEnergyCountdown()
    {
        if (!EnergyManager.UserHasMaxEnergy())
        {
            //FIXME: Display Energy Countdown
        }
    }

}