using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    // UI
    public GameObject playerPrefab;
    public GameObject playerProfile;
    public GameObject playersContainer;

    // variables
    string flagName = "FRA";
    Fighter player;
    Dictionary<string, Dictionary<string, string>> usersDB;

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
        playerPrefab = GameObject.Find("List_Me");
        playersContainer = GameObject.Find("Content");

        // user
        player = PlayerUtils.FindInactiveFighter();
        SetupPlayer();

        // ranking
        GetDB();
        GenerateDB();
    }

    private void Update()
    {
        if (CanUpdateLeaderboard())
        {
            UpdateDB();
            LeaderboardDB.UpdateDB();
        }
    }

    private void GetDB()
    {
        usersDB = LeaderboardDB.players;
    }

    private void GenerateDB()
    {
        foreach (KeyValuePair<string, Dictionary<string, string>> user in usersDB)
        {
            Debug.Log(user.Key);
            SetupOtherPlayer(user);
            // set up prefab
            Instantiate(playerPrefab, playerPrefab.transform.parent);
        }
    }

    private void UpdateDB()
    {
        PlayerPrefs.SetString("leaderboardUpdate", DateTime.Now.AddSeconds(10).ToBinary().ToString());
        PlayerPrefs.Save();
    }

    public bool CanUpdateLeaderboard()
    {
        if (PlayerPrefs.GetString("leaderboardUpdate") != "")
            return DateTime.Compare(DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("leaderboardUpdate"))), DateTime.Now) <= 0;

        return false;
    }

    private void SetupOtherPlayer(KeyValuePair<string, Dictionary<string, string>> user)
    {
        playerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = GetFlagByName(user.Value["country"]);

        playerPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = player.fighterName;

        playerPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = User.Instance.elo.ToString();

        playerPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 1.ToString();

        playerPrefab.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = MenuUtils.GetProfilePicture(player.species);

        // if it's top 3 enable medal and disable text
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
