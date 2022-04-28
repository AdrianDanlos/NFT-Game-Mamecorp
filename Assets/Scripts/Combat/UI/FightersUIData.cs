using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightersUIData : MonoBehaviour
{
    public GameObject playerNameGO;
    public GameObject playerEloGO;
    public GameObject playerHealthBarGO;
    public GameObject botEloGO;
    public GameObject botNameGO;
    public GameObject botHealthBarGO;
    private float playerMaxHealth;
    private float botMaxHealth;
    public GameObject resultsEloChange;
    public GameObject playerLevelGO;
    public GameObject playerExpGO;
    public GameObject playerLevelSliderGO;
    public GameObject playerExpGainTextGO;
    public GameObject levelUpIcon;
    public GameObject victoryBanner;
    public GameObject defeatBanner;
    public GameObject goldRewardGO;
    public GameObject gemsRewardGO;
    public GameObject chestRewardGO;

    public void ShowPostCombatInfo(Fighter player, bool isPlayerWinner, int eloChange, bool isLevelUp, int goldReward, int gemsReward, Canvas results)
    {
        SetResultsBanner(isPlayerWinner);
        SetResultsEloChange(eloChange);
        SetResultsLevel(player.level, player.experiencePoints);
        SetResultsExpGainText(isPlayerWinner);
        ShowLevelUpIcon(isLevelUp);
        ShowRewards(goldReward, gemsReward, isLevelUp);
        EnableResults(results);
    }

    public void SetFightersUIInfo(Fighter player, Fighter bot, int botElo)
    {
        SetFightersElo(botElo);
        SetFightersName(player.fighterName, bot.fighterName);
        SetFightersMaxHealth(player.hp, bot.hp);
    }

    private void SetFightersElo(int botElo)
    {
        playerEloGO.GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();
        botEloGO.GetComponent<TextMeshProUGUI>().text = botElo.ToString();
    }

    private void SetFightersName(string playerName, string botName)
    {
        playerNameGO.GetComponent<TextMeshProUGUI>().text = playerName;
        botNameGO.GetComponent<TextMeshProUGUI>().text = botName;
    }

    public void SetFightersMaxHealth(float playerMaxHealth, float botMaxHealth)
    {
        this.playerMaxHealth = playerMaxHealth;
        this.botMaxHealth = botMaxHealth;
    }

    public void ModifyHealthBar(Fighter fighter, bool isPlayerTargetOfHealthChange)
    {
        if (isPlayerTargetOfHealthChange)
        {
            SetHealthBarValue(playerHealthBarGO, fighter, playerMaxHealth);
            return;
        }

        SetHealthBarValue(botHealthBarGO, fighter, botMaxHealth);
    }

    private void SetHealthBarValue(GameObject healthBar, Fighter fighter, float maxHealth)
    {
        healthBar.GetComponent<Slider>().value = fighter.hp / maxHealth;
    }

    public void SetResultsEloChange(int eloChange)
    {
        resultsEloChange.GetComponent<TextMeshProUGUI>().text = eloChange > 0 ? $"+{eloChange.ToString()}" : eloChange.ToString();
    }
    public void SetResultsLevel(int playerLevel, int playerExp)
    {
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSliderGO, playerLevel, playerExp);
        MenuUtils.DisplayLevelIcon(playerLevel, GameObject.FindGameObjectWithTag("ResultsLevelIcons"));
    }
    public void SetResultsExpGainText(bool isPlayerWinner)
    {
        playerExpGainTextGO.GetComponent<TextMeshProUGUI>().text = $"+{Levels.GetXpGain(isPlayerWinner).ToString()}";
    }
    public void ShowLevelUpIcon(bool isLevelUp)
    {
        levelUpIcon.SetActive(isLevelUp);
    }

    public void EnableResults(Canvas results)
    {
        results.enabled = true;
    }
    public void SetResultsBanner(bool isPlayerWinner)
    {
        victoryBanner.SetActive(isPlayerWinner);
        defeatBanner.SetActive(!isPlayerWinner);
    }

    public void ShowRewards(int goldReward, int gemsReward, bool isLevelUp)
    {
        gemsRewardGO.SetActive(Convert.ToBoolean(gemsReward));
        // generate chess here and set active correct chest 
        // atm there is an static image
        chestRewardGO.SetActive(isLevelUp);

        goldRewardGO.transform.Find("TextValue").gameObject.GetComponent<TextMeshProUGUI>().text = goldReward.ToString();
        if (Convert.ToBoolean(gemsReward)) gemsRewardGO.transform.Find("TextValue").gameObject.GetComponent<TextMeshProUGUI>().text = gemsReward.ToString();
    }
}
