using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* CUP STRUCTURE IDS
 * 
 * QUARTERS     SEMIS       FINAL       SEMIS       QUARTERS
 * 
 *    1            9         13          11            5
 *    2           10         14          12            6
 * 
 *    3                                                7
 *    4                                                8
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

    // scripts
    CupManager cupManager;

    // vars
    public string round;

    private void Awake()
    {
        labelContainer = GameObject.Find("LabelContainer").GetComponent<Transform>();
        playersContainer = GameObject.Find("Players").GetComponent<Transform>();
        roundAnnouncer = GameObject.Find("RoundAnnouncerTxt").GetComponent<TextMeshProUGUI>();
        cupManager = GetComponent<CupManager>();

        HideCupLabels();
        GetAllUIPlayers();

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

    private void DisplayPlayerQuarters()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfoDictionary = Cup.Instance.cupInfo;
        var participantsList = Cup.Instance.participants;

        playersContainer.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        int counter = 0;

        /* 
            var participant = from s in participantsList
                        where s.id == participantsList[counter].id
                        select s;
        */

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Quarters"))
            {
                player.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    GetSpeciePortrait(participantsList[counter].species);
                player.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    participantsList[counter].fighterName;

                counter++;
            }
        }
    }

    private void DisplayPlayerSemis()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfoDictionary = Cup.Instance.cupInfo;

        playersContainer.GetChild(8).GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        int counter = 0;
        List<CupFighter> _participants = cupManager.GenerateParticipantsBasedOnQuarters();

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Semis"))
            {
                player.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    GetSpeciePortrait(_participants[counter].species);
                player.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    _participants[counter].fighterName;

                counter++;
            }
        }

        cupManager.GrayOutLosersQuarters();
    }

    private void DisplayPlayerFinals()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfoDictionary = Cup.Instance.cupInfo;

        playersContainer.GetChild(12).GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        int counter = 0;
        List<CupFighter> _participants = cupManager.GenerateParticipantsBasedOnSemis();

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Finals"))
            {
                player.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    GetSpeciePortrait(_participants[counter].species);
                player.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    _participants[counter].fighterName;

                counter++;
            }
        }

        cupManager.GrayOutLosersSemis();
    }

    private void DisplayPlayerFinalsEnd()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfoDictionary = Cup.Instance.cupInfo;

        playersContainer.GetChild(12).GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        int counter = 0;
        List<CupFighter> _participants = cupManager.GenerateParticipantsBasedOnSemis();

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Finals"))
            {
                player.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                    GetSpeciePortrait(_participants[counter].species);
                player.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    _participants[counter].fighterName;

                counter++;
            }
        }

        cupManager.GrayOutLoserFinals();
    }

    private void SetUIBasedOnRound()
    {
        Debug.Log(Cup.Instance.round);
        switch (Cup.Instance.round)
        {
            case "quarters":
                SetUIQuarters();
                DisplayPlayerQuarters();
                break;
            case "semis":
                SetUISemis();
                DisplayPlayerQuarters();
                DisplayPlayerSemis();
                break;
            case "finals":
                SetUIFinals();
                DisplayPlayerQuarters();
                DisplayPlayerSemis();
                DisplayPlayerFinals();
                break;
            case "end":
                SetUIFinalsEnd();
                DisplayPlayerQuarters();
                DisplayPlayerSemis();
                DisplayPlayerFinals();
                DisplayPlayerFinalsEnd();
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

    private void SetUIFinals()
    {
        roundAnnouncer.text = "FINALS";
    }

    private void SetUIFinalsEnd()
    {
        roundAnnouncer.text = "TOURNAMENT ENDED";
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

    private Sprite GetSpeciePortrait(string species)
    {
        return Resources.Load<Sprite>("CharacterProfilePicture/" + species);
    }
}
