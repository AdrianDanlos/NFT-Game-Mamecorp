using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

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
    public GameObject nextButtonGO;
    
    // health bar animations
    public GameObject playerHealthBarFadeGO;
    public GameObject botHealthBarFadeGO;
    public GameObject playerPortrait;
    public GameObject botPortrait;
    public GameObject playerPortraitFrame;
    public GameObject botPortraitFrame;
    private float previousPlayerHp = 1f;
    private float previousBotHp = 1f;

    // usericons
    public GameObject playerIcon;
    public GameObject botIcon;

    private void AddListenerToNextBtn(bool isLevelUp) {
        nextButtonGO.GetComponent<Button>().onClick.AddListener(() => OnClickNextHandler(isLevelUp));
    }

    private void OnClickNextHandler(bool isLevelUp){
        if(isLevelUp) UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.LevelUp.ToString());
        else UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu.ToString());
    }

    public void ShowPostCombatInfo(Fighter player, bool isPlayerWinner, int eloChange, bool isLevelUp, int goldReward, int gemsReward, Canvas results)
    {
        AddListenerToNextBtn(isLevelUp);
        SetResultsBanner(isPlayerWinner);
        SetResultsEloChange(eloChange);
        SetResultsLevel(player.level, player.experiencePoints);
        SetResultsExpGainText(isPlayerWinner);
        ShowLevelUpIcon(isLevelUp);
        ShowRewards(goldReward, gemsReward);
        EnableResults(results);
    }

    public void SetFightersUIInfo(Fighter player, Fighter bot, int botElo)
    {
        SetFightersElo(botElo);
        SetFightersName(player.fighterName, bot.fighterName);
        SetFightersMaxHealth(player.hp, bot.hp);
        GetComponent<Combat>().SetFightersPortrait(playerPortrait, botPortrait);
        SetPlayerIcons(playerIcon, botIcon);
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

    private void SetPlayerIcons(GameObject playerIcon, GameObject botIcon)
    {
        playerIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/UserIcons/" + User.Instance.userIcon);
        botIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/UserIcons/" + UnityEngine.Random.Range(0, Resources.LoadAll<Sprite>("Icons/UserIcons/").Length));
    }

    public void HidePortraitsUI()
    {
        playerPortrait.SetActive(false);
        botPortrait.SetActive(false);
        playerPortraitFrame.SetActive(false);
        botPortraitFrame.SetActive(false);
    }

    public void ShowPortraitsUI()
    {
        playerPortrait.SetActive(true);
        botPortrait.SetActive(true);
        playerPortraitFrame.SetActive(true);
        botPortraitFrame.SetActive(true);
    }

    public void ModifyHealthBar(Fighter fighter, bool isPlayerTargetOfHealthChange)
    {
        if (isPlayerTargetOfHealthChange)
        {
            SetHealthBarValue(isPlayerTargetOfHealthChange, playerHealthBarFadeGO, playerHealthBarGO, fighter, playerMaxHealth);
            FighterHitPortraitAnimation(isPlayerTargetOfHealthChange);
            return;
        }

        SetHealthBarValue(isPlayerTargetOfHealthChange, botHealthBarFadeGO, botHealthBarGO, fighter, botMaxHealth);
        FighterHitPortraitAnimation(isPlayerTargetOfHealthChange);
    }

    private void SetHealthBarValue(bool isPlayer, GameObject healthBarFade, GameObject healthBar, Fighter fighter, float maxHealth)
    {
        StartCoroutine(HealthAnimation(isPlayer, fighter.hp, maxHealth, healthBarFade));
        healthBar.GetComponent<Slider>().value = fighter.hp / maxHealth;
    }

    IEnumerator HealthAnimation(bool isPlayer, float health, float maxHealth, GameObject healthBarFade)
    {
        Image healthBarFadeSliderValue = healthBarFade.GetComponent<Image>();
        float newHp = health / maxHealth;

        if (newHp > 0)
        {
            do
            {
                yield return new WaitForSeconds(0.2f);
                if (isPlayer)
                    healthBarFadeSliderValue.fillAmount -= (previousPlayerHp - newHp) / 5;
                else
                    healthBarFadeSliderValue.fillAmount -= (previousBotHp - newHp) / 5;

            } while (healthBarFadeSliderValue.fillAmount > newHp);
        }

        if (isPlayer)
            previousPlayerHp = newHp;
        else
            previousBotHp = newHp;
    }

    public void FighterHitPortraitAnimation(bool isPlayerTargetOfHealthChange)
    {
        // TODO dont do animation when health restored
        // need to do double animation to fix not playing onAwake
        if (isPlayerTargetOfHealthChange)
        {
            playerPortraitFrame.GetComponent<Animator>().SetTrigger("GetDamage");
            playerPortraitFrame.GetComponent<Animator>().SetTrigger("GetDamage");
            return;
        }

        botPortraitFrame.GetComponent<Animator>().SetTrigger("GetDamage");
        botPortraitFrame.GetComponent<Animator>().SetTrigger("GetDamage");
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

    public void ShowRewards(int goldReward, int gemsReward)
    {
        gemsRewardGO.SetActive(Convert.ToBoolean(gemsReward));
        goldRewardGO.transform.Find("TextValue").gameObject.GetComponent<TextMeshProUGUI>().text = goldReward.ToString();
        if (Convert.ToBoolean(gemsReward)) gemsRewardGO.transform.Find("TextValue").gameObject.GetComponent<TextMeshProUGUI>().text = gemsReward.ToString();
    }
}
