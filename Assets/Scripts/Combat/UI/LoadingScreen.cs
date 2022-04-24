using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public GameObject playerName;
    public GameObject botName;
    
    //Player should have only idle skin (choose correct one. We already have code that does this e.g. main menu)
    //Enemy should display either a sequence of the 3 skins on black or a loader. After x time show everything

    public void SetLoadingScreenData(Fighter player, Fighter bot){
        playerName.GetComponent<TextMeshProUGUI>().text = player.fighterName;
        botName.GetComponent<TextMeshProUGUI>().text = bot.fighterName;
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("PlayerLoadingScreenLevels"));
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("BotLoadingScreenLevels"));
    }
}