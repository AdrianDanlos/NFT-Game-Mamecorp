using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MenuUtils {
    public static void SetName(GameObject playerNameGO)
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = User.Instance.userName;
    }
    public static void SetLevel(GameObject playerLevelGO, int playerLevel)
    {
        playerLevelGO.GetComponent<TextMeshProUGUI>().text = playerLevel.ToString();
    }
    public static void SetExperiencePoints(GameObject playerExpGO, int playerLevel, int playerExp)
    {
        int maxExp = Levels.MaxXpOfCurrentLevel(playerLevel);
        playerExpGO.GetComponent<TextMeshProUGUI>().text = $"{playerExp.ToString()}/{maxExp}";
    }
    public static void SetSlider(GameObject playerLevelSliderGO, int playerLevel, int playerExp)
    {
        int maxExp = Levels.MaxXpOfCurrentLevel(playerLevel);
        playerLevelSliderGO.GetComponent<Slider>().value = (float)playerExp / (float)maxExp;
    }

    public static void SetGold(GameObject goldGO)
    {
        goldGO.GetComponent<TextMeshProUGUI>().text = User.Instance.gold.ToString();
    }

    public static void SetEnergy(GameObject energyGO)
    {
        //FIXME: Set max energy here
        energyGO.GetComponent<TextMeshProUGUI>().text = $"{User.Instance.energy.ToString()}/{10}";
    }
}