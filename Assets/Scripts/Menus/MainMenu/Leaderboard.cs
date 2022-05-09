using System.Collections;
using System.Collections.Generic;
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
        player = PlayerUtils.FindInactiveFighter();
    }

    private void GetAllPlayers()
    {
        for(int i = 0; i < playersContainer.transform.childCount; i++)
            playersList.Add(playersContainer.transform.GetChild(i));
    }

    private void SetupPlayer()
    {
        ChangeUserFlag(LeaderboardDB.Flag.ESP.ToString());
    }

    private void ChangeUserFlag(string flagName)
    {
        playerProfile.transform.GetChild(2).GetComponent<Image>().sprite = GetFlagByName(flagName);
    }

    private Sprite GetFlagByName(string flagName)
    {
        return Resources.Load<Sprite>("Flags/Icon_Flag_" + flagName);
    }
}
