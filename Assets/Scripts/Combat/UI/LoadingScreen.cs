using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public GameObject playerName;
    public GameObject botName;
    public GameObject botSprite;
    public GameObject botData;
    public GameObject botLevels;
    public GameObject spinner;

    public void SetPlayerLoadingScreenData(Fighter player)
    {
        playerName.GetComponent<TextMeshProUGUI>().text = player.fighterName;
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("PlayerLoadingScreenLevels"));
    }

    public void DisplayLoaderForEnemy()
    {
        GetGameObjects();
        ToggleSpinnerAndBotData(false, true);
    }

    public void SetBotLoadingScreenData(Fighter bot)
    {
        botName.GetComponent<TextMeshProUGUI>().text = bot.fighterName;
        MenuUtils.DisplayLevelIcon(bot.level, botLevels);
        ToggleSpinnerAndBotData(true, false);
    }

    public void ToggleSpinnerAndBotData(bool showBot, bool showSpinner)
    {
        botSprite.SetActive(showBot);
        botData.SetActive(showBot);
        spinner.SetActive(showSpinner);
    }

    private void GetGameObjects()
    {
        botSprite = GameObject.FindGameObjectWithTag("LoadingScreenBot");
        botData = GameObject.FindGameObjectWithTag("CombatLoadingScreenBotData");
        botLevels = GameObject.Find("BotLoadingScreenLevels");
        spinner = GameObject.FindGameObjectWithTag("CombatLoadingScreenSpinner");
    }


}