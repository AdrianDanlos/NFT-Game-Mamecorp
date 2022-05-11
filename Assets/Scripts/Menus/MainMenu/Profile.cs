using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    // UI
    GameObject levelBar;
    GameObject levelExp;
    GameObject trophiesText;
    GameObject cupsText;
    GameObject nCombatsText;
    GameObject highestEnemyText;
    GameObject userName;
    GameObject characterProfilePicture;
    GameObject userIcon;

    // Components
    Fighter player;

    void Awake()
    {
        player = PlayerUtils.FindInactiveFighter();

        // UI
        levelBar = GameObject.Find("LevelBar");
        levelExp = GameObject.Find("Level_Text_Exp");
        trophiesText = GameObject.Find("Trophies_Text_Value");
        cupsText = GameObject.Find("Cups_Text_Value");
        nCombatsText = GameObject.Find("NCombats_Text_Value");
        highestEnemyText = GameObject.Find("Highest_Enemy_Text_Value");
        userName = GameObject.Find("Text_UserName");
        characterProfilePicture = GameObject.Find("Character_Picture");
        userIcon = GameObject.Find("UserIconImage");

        LoadStats();
    }

    private void LoadStats()
    {
        MenuUtils.SetLevelSlider(levelExp, levelBar, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, levelBar);
        MenuUtils.SetName(userName, player.fighterName);
        MenuUtils.SetProfilePicture(characterProfilePicture);
        MenuUtils.SetProfileUserIcon(userIcon);

        // TODO CUPS
        cupsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("cups").ToString();
        nCombatsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("fights").ToString();
        trophiesText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("maxTrophies").ToString();
        highestEnemyText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("highestEnemy").ToString();
    }
}
