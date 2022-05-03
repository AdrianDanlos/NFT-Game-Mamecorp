using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    // UI
    public List<Transform> playersList;
    public GameObject playerProfile;
    public GameObject playersContainer;

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
    }

    private void GetAllPlayers()
    {
        for(int i = 0; i < playersContainer.transform.childCount; i++)
            playersList.Add(playersContainer.transform.GetChild(i));
    }
}
