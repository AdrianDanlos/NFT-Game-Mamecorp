using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    Dictionary<string, string> orderedDB;

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
        }
    }

    private void GetDB()
    {
        usersDB = LeaderboardDB.players;
    }

    private void GenerateDB()
    {
        int ranking = 1;

        foreach (KeyValuePair<string, Dictionary<string, string>> user in usersDB)
        {
            SetupOtherPlayer(user, ranking);
            Instantiate(playerPrefab, playersContainer.transform);
            ranking++;
        }

        OrderDB();
    }

    private void OrderDB()
    {
        orderedDB = new Dictionary<string, string>();

        foreach (KeyValuePair<string, Dictionary<string, string>> user in usersDB)
            orderedDB.Add(user.Key, user.Value["trophies"]);

        foreach (KeyValuePair<string, string> user in orderedDB.OrderBy(key => key.Value))
            Debug.Log("Key: {0}, Value: {1}" +  user.Key + user.Value);
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

    private void SetupOtherPlayer(KeyValuePair<string, Dictionary<string, string>> user, int ranking)
    {
        playerPrefab.transform.GetChild(2).GetComponent<Image>().sprite = GetFlagByName(user.Value["country"]);
        playerPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = user.Value["name"];
        playerPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = user.Value["trophies"];
        playerPrefab.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = MenuUtils.GetProfilePicture(user.Value["specie"]);

        if (ranking == 1)
        {
            playerPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            playerPrefab.transform.GetChild(6).GetComponent<Image>().enabled = true;
            playerPrefab.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Medals/Medal_Gold");
        }

        if (ranking == 2)
        {
            playerPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            playerPrefab.transform.GetChild(6).GetComponent<Image>().enabled = true;
            playerPrefab.transform.GetChild(6).GetComponent<RectTransform>().sizeDelta = new Vector2(111, 108);
            playerPrefab.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Medals/Medal_Silver");
        }
        if (ranking == 3)
        {
            playerPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            playerPrefab.transform.GetChild(6).GetComponent<Image>().enabled = true;
            playerPrefab.transform.GetChild(6).GetComponent<RectTransform>().sizeDelta = new Vector2(82, 108);
            playerPrefab.transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Medals/Medal_Bronze");
        }
        if (ranking > 3)
        {
            playerPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;
            playerPrefab.transform.GetChild(6).GetComponent<Image>().enabled = false;
            playerPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ranking.ToString();
        }
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
