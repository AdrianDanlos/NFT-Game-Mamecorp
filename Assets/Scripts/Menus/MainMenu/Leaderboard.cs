using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    // UI
    public List<Transform> playersList;
    public GameObject playerProfile;
    public GameObject playersContainer;

    string flagName;
    Fighter player;
    

    // Player GameObject Structure
    // - List_Me
    //      - icon/level text
    //      - character_Bg
    //          - mask
    //              - character image
    //      - icon country
    //      - nickname
    //      - trophies text

    private void Awake()
    {
        playerProfile = GameObject.Find("List_Me_Player");
        playersContainer = GameObject.Find("Content");

        GetAllPlayers();

        // user
        player = PlayerUtils.FindInactiveFighter();
        SetupPlayer();
    }

    private void GetAllPlayers()
    {
        for(int i = 0; i < playersContainer.transform.childCount; i++)
            playersList.Add(playersContainer.transform.GetChild(i));
    }

    private void SetupPlayer()
    {
        SetUpUserFlag(LeaderboardDB.Flag.ESP.ToString());
        SetupUserName();
        SetupUserTrophies();
        SetupUserRanking();
        SetupUserSprite();
    }

    private void SetUpUserFlag(string flagName)
    {
        playerProfile.transform.GetChild(2).GetComponent<Image>().sprite = GetFlagByName(flagName);
    }

    private Sprite GetFlagByName(string flagName)
    {
        return Resources.Load<Sprite>("Flags/Icon_Flag_" + flagName);
    }

    private void SetupUserName()
    {
        playerProfile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = player.fighterName;
    }

    private void SetupUserTrophies()
    {
        playerProfile.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();
    }

    private void SetupUserRanking()
    {
        // TODO calc ranking
        playerProfile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 1.ToString();
    }

    private void SetupUserSprite()
    {
        MenuUtils.SetProfilePicture(playerProfile.transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
    }
}
