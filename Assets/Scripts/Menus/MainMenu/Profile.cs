using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    // UI
    public GameObject playerLevelSlider;
    public GameObject playerExpGO;
    GameObject trophiesText;
    GameObject cupsText;
    GameObject nCombatsText;
    GameObject winrate;
    GameObject wins;
    GameObject loses;
    GameObject userName;
    GameObject characterProfilePicture;
    GameObject userIcon;

    // Components
    Fighter player;

    void Awake()
    {
        player = PlayerUtils.FindInactiveFighter();

        // UI
        trophiesText = GameObject.Find("Trophies_Text_Value");
        cupsText = GameObject.Find("Cups_Text_Value");
        nCombatsText = GameObject.Find("NCombats_Text_Value");
        winrate = GameObject.Find("winrate");
        wins = GameObject.Find("wins");
        loses = GameObject.Find("loses");
        userName = GameObject.Find("Text_UserName");
        characterProfilePicture = GameObject.Find("Character_Picture");
        userIcon = GameObject.Find("UserIconImage");

        LoadStats();
    }

    private void LoadStats()
    {      
        MenuUtils.SetLevelSlider(playerExpGO, playerLevelSlider, player.level, player.experiencePoints);
        MenuUtils.DisplayLevelIcon(player.level, GameObject.Find("Levels_Profile"));
        MenuUtils.SetName(userName, player.fighterName);
        MenuUtils.SetProfilePicture(characterProfilePicture);
        MenuUtils.SetProfileUserIcon(userIcon);

        //FIXME: Are these saved in playerprefs?
        trophiesText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("maxTrophies").ToString();
        cupsText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("cups").ToString();
        winrate.GetComponent<TextMeshProUGUI>().text = GetWinrate().ToString();
        nCombatsText.GetComponent<TextMeshProUGUI>().text = (User.Instance.wins + User.Instance.loses).ToString();
        wins.GetComponent<TextMeshProUGUI>().text = User.Instance.wins.ToString();
        loses.GetComponent<TextMeshProUGUI>().text = User.Instance.loses.ToString();

    }

    private int GetWinrate(){
        if(User.Instance.wins + User.Instance.loses == 0)
            return 0;
        else
            return User.Instance.wins / (User.Instance.wins + User.Instance.loses) * 100;
    }
}
