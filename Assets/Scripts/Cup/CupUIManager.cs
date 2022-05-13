using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* CUP STRUCTURE IDS
 * 
 * QUARTERS     SEMIS       FINAL       SEMIS       QUARTERS
 * 
 *    1           10         14          12            6
 *    2           11         15          13            7
 * 
 *    3                                                8
 *    4                                                9
 *    
 * ----------------------------------------------------------
 * 
 * MATCHES STRUCTURE IDS
 * 
 * QUARTERS     SEMIS       FINAL       SEMIS       QUARTERS
 * 
 *    1           5           7           6            3
 *                                              
 *    2                                                4
 *                                                    
 *    
 * ----------------------------------------------------------
 * 
 * Players gameobject structure
 * 
 * - BG
 *      - mask
 *          - species portrait
 * - nickname
 * - BGFade (when eliminated)
 * 
 */

public class CupUIManager : MonoBehaviour
{
    // UI
    Transform labelContainer;
    Transform playersContainer;
    List<Transform> participants;
    TextMeshProUGUI roundAnnouncer;

    // vars
    public string round;

    private void Awake()
    {
        labelContainer = GameObject.Find("LabelContainer").GetComponent<Transform>();
        playersContainer = GameObject.Find("Players").GetComponent<Transform>();
        roundAnnouncer = GameObject.Find("RoundAnnouncerTxt").GetComponent<TextMeshProUGUI>();

        HideCupLabels();
        GetAllUIPlayers();

        DisplayPlayers();
        SetUIBasedOnRound();
    }

    private void Start()
    {
        ShowCupLabel();
    }

    private void GetAllUIPlayers()
    {
        participants = new List<Transform>();

        for (int i = 0; i < playersContainer.childCount; i++)
            participants.Add(playersContainer.GetChild(i));
    }

    private void DisplayPlayers()
    {
        
    }

    private void SetUIBasedOnRound()
    {
        Debug.Log(Cup.Instance.round);
        switch (Cup.Instance.round)
        {
            case "quarters":
                SetUIQuarters();
                break;
            case "semis":
                SetUISemis();
                break;
            case "finals":

                break;
        }
    }

    private void SetUIQuarters()
    {
        roundAnnouncer.text = "QUARTERS";

        foreach(Transform player in participants)
        {
            if (player.name.Contains("Semis") || player.name.Contains("Finals"))
            {
                player.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0);
                player.GetChild(1).GetComponent<TextMeshProUGUI>().text = "???";
            }
        }
    }

    private void SetUISemis()
    {
        roundAnnouncer.text = "SEMIFINALS";

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Finals"))
            {
                player.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0);
                player.GetChild(1).GetComponent<TextMeshProUGUI>().text = "???";
            }
        }
    }

    private void HideCupLabels()
    {
        for(int i = 0; i < labelContainer.childCount; i++)
            labelContainer.GetChild(i).gameObject.SetActive(false);
    }

    private Transform GetCupLabelByName(string name)
    {
        switch(name)
        {
            case "FIRE":
                return labelContainer.GetChild(0);
            case "AIR":
                return labelContainer.GetChild(1);
            case "EARTH":
                return labelContainer.GetChild(2);
            case "WATER":
                return labelContainer.GetChild(3);
        }

        Debug.Log("Error!");
        return labelContainer.GetChild(0);
    }

    private void ShowCupLabel()
    {
        GetCupLabelByName(Cup.Instance.cupName).gameObject.SetActive(true);
    }
}
