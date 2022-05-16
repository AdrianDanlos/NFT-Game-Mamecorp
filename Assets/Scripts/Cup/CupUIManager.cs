using System.Collections;
using System.Collections.Generic;
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
    Button buttonBattle;

    // scripts
    CupManager cupManager;

    // vars
    public string round;
    private Color32 playerHihglight = new Color32(254, 161, 0, 255);

    private void Awake()
    {
        labelContainer = GameObject.Find("LabelContainer").GetComponent<Transform>();
        playersContainer = GameObject.Find("Players").GetComponent<Transform>();
        roundAnnouncer = GameObject.Find("RoundAnnouncerTxt").GetComponent<TextMeshProUGUI>();
        buttonBattle = GameObject.Find("Button_Cup").GetComponent<Button>();
        cupManager = GetComponent<CupManager>();

        IsTournamentOver();

        HideCupLabels();
        GetAllUIPlayers();

        SetUIBasedOnRound();
    }

    private IEnumerator Start()
    {
        ShowCupLabel();

        if (SceneFlag.sceneName == SceneNames.Combat.ToString() || SceneFlag.sceneName == SceneNames.LevelUp.ToString()) 
        {
            yield return new WaitForSeconds(GeneralUtils.GetRealOrSimulationTime(1f));
            StartCoroutine(SceneManagerScript.instance.FadeIn());
        }

        SceneFlag.sceneName = SceneNames.Cup.ToString();
    }

    private void GetAllUIPlayers()
    {
        participants = new List<Transform>();

        for (int i = 0; i < playersContainer.childCount; i++)
            participants.Add(playersContainer.GetChild(i));
    }

    private void IsTournamentOver()
    {
        if (Cup.Instance.round == CupDB.CupRounds.END.ToString() || !Cup.Instance.isActive)
            buttonBattle.gameObject.SetActive(false);
    }

    private void DisplayPlayerQuarters()
    {
        var participantsList = Cup.Instance.participants;

        playersContainer.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = playerHihglight;
        int counter = 0;

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

                if(_participants[counter].id == "0")
                    playersContainer.GetChild(8).GetChild(1).GetComponent<TextMeshProUGUI>().color = playerHihglight;

                counter++;
            }
        }

        GrayOutLosersQuarters();
    }

    private void DisplayPlayerFinals()
    {
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

                if (_participants[counter].id == "0")
                    playersContainer.GetChild(12).GetChild(1).GetComponent<TextMeshProUGUI>().color = playerHihglight;

                counter++;
            }
        }

        GrayOutLosersSemis();
    }

    private void DisplayPlayerFinalsEnd()
    {
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

                if (_participants[counter].id == "0")
                    playersContainer.GetChild(12).GetChild(1).GetComponent<TextMeshProUGUI>().color = playerHihglight;

                counter++;
            }
        }

        GrayOutLoserFinals();
    }

    private void SetUIBasedOnRound()
    {
        switch (Cup.Instance.round)
        {
            case "QUARTERS":
                SetUIQuarters();
                DisplayPlayerQuarters();
                break;
            case "SEMIS":
                SetUISemis();
                DisplayPlayerQuarters();
                DisplayPlayerSemis();
                break;
            case "FINALS":
                SetUIFinals();
                DisplayPlayerQuarters();
                DisplayPlayerSemis();
                DisplayPlayerFinals();
                break;
            case "END":
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
        roundAnnouncer.text = CupDB.CupRounds.QUARTERS.ToString();

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
        roundAnnouncer.text = CupDB.CupRounds.SEMIS.ToString(); ;

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
        roundAnnouncer.text = CupDB.CupRounds.FINALS.ToString(); 
    }

    private void SetUIFinalsEnd()
    {
        string winnerId = Cup.Instance.cupInfo[CupDB.CupRounds.FINALS.ToString()]["7"]["winner"];
        string winnerName = "";
        foreach (CupFighter fighter in Cup.Instance.participants)
        {
            if (fighter.id == winnerId)
                winnerName = fighter.fighterName;
        }

        roundAnnouncer.text = "TOURNAMENT ENDED\n" + "WINNER " + winnerName + "!";
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

    public void GrayOutLosersQuarters()
    {
        var participantsList = Cup.Instance.participants;
        var cupInfo = Cup.Instance.cupInfo;
        List<string> loserIds = new List<string>();
        int counter = 5; // match ids + 1

        for (int i = 1; i < counter; i++)
            loserIds.Add(cupInfo[CupDB.CupRounds.QUARTERS.ToString()][i.ToString()]["loser"]);

        counter = 0;

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Quarters"))
            {
                for(int i = 0; i < loserIds.Count; i++)
                {
                    if (participantsList[counter].id == loserIds[i])
                    {
                        player.GetChild(2).GetComponent<Image>().enabled = true;
                    }
                }

                counter++;
            }
        }
    }

    public void GrayOutLosersSemis()
    {
        var participantsList = cupManager.GenerateParticipantsBasedOnQuarters();
        var cupInfo = Cup.Instance.cupInfo;
        List<string> loserIds = new List<string>();
        int counter = 7; // match ids + 1

        for (int i = 5; i < counter; i++)
            loserIds.Add(cupInfo[CupDB.CupRounds.SEMIS.ToString()][i.ToString()]["loser"]);

        counter = 0;

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Semis"))
            {
                for (int i = 0; i < loserIds.Count; i++)
                {
                    if (participantsList[counter].id == loserIds[i])
                    {
                        player.GetChild(2).GetComponent<Image>().enabled = true;
                    }
                }

                counter++;
            }
        }
    }

    public void GrayOutLoserFinals()
    {
        var participantsList = cupManager.GenerateParticipantsBasedOnSemis();
        var cupInfo = Cup.Instance.cupInfo;
        List<string> loserIds = new List<string>();
        int counter = 7; // match ids + 1 

        loserIds.Add(cupInfo[CupDB.CupRounds.FINALS.ToString()][counter.ToString()]["loser"]);

        counter = 0;

        foreach (Transform player in participants)
        {
            if (player.name.Contains("Finals"))
            {
                for (int i = 0; i < loserIds.Count; i++)
                {
                    if (participantsList[counter].id == loserIds[i])
                    {
                        player.GetChild(2).GetComponent<Image>().enabled = true;
                    }
                }

                counter++;
            }
        }
    }
}
