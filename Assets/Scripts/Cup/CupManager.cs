using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CupManager : MonoBehaviour
{
    private void Awake()
    {
        bool saveFileFound = File.Exists(JsonDataManager.getFilePath(JsonDataManager.CupFileName));

        if (saveFileFound)
            JsonDataManager.ReadCupFile();
        else
            CreateCupFile();
    }

    private void CreateCupFile()
    {
        Array cupNames = Enum.GetValues(typeof(CupDB.CupNames));
        System.Random random = new System.Random();
        string cupName = cupNames.GetValue(random.Next(cupNames.Length)).ToString();
        string round = "quarters";
        List<CupFighter> participants = GenerateParticipants();
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = GenerateCupInitialInfo();

        CupFactory.CreateCupInstance(cupName, round, participants, cupInfo);
        JObject cup = JObject.FromObject(Cup.Instance);
        JsonDataManager.SaveData(cup, JsonDataManager.CupFileName);
    }

    private List<CupFighter> GenerateParticipants()
    {
        Fighter player = PlayerUtils.FindInactiveFighter();

        // there will be 8 fighters per cup (7 + user)
        List<CupFighter> participants = new List<CupFighter>();

        CupFighter user = new CupFighter(0.ToString(), player.fighterName, player.species);
        participants.Add(user);

        for(int i = 1; i < 8; i++)
        {
            participants.Add(
                new CupFighter(
                    i.ToString(),
                    RandomNameGenerator.GenerateRandomName(),
                    GeneralUtils.GetRandomSpecies()
                )
            );
        }

        return participants;
    }

    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> GenerateCupInitialInfo()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = 
            new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
            {
                { "quarters", new Dictionary<string, Dictionary<string, string>>
                    {
                        { "1", new Dictionary<string, string>
                            {
                                { "matchId", "1"} , // match id
                                { "1", "0"} ,       // seed 1 player
                                { "2", "1"} ,       // seed 2 player
                                { "winner" , ""}    // winner id
                            }
                        },
                        { "2", new Dictionary<string, string>
                            {
                                { "matchId", "2"} ,
                                { "3", "2"} ,
                                { "4", "3"} ,
                                { "winner" , ""}
                            }
                        },
                        { "3", new Dictionary<string, string>
                            {
                                { "matchId", "3"} ,
                                { "5", "4"} ,
                                { "6", "5"} ,
                                { "winner" , ""}
                            }
                        },
                        { "4", new Dictionary<string, string>
                            {
                                { "matchId", "4"} ,
                                { "7", "6"} ,
                                { "8", "7"} ,
                                { "winner" , ""}
                            }
                        },
                    }
                }
            };

        return cupInfo;
    }


    public void SimulateQuarters()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = Cup.Instance.cupInfo;
        List<string> match1 = new List<string>
        {
            cupInfo["quarters"]["1"]["1"],
            cupInfo["quarters"]["1"]["2"],
        };

        List<string> match2 = new List<string>
        {
            cupInfo["quarters"]["2"]["3"],
            cupInfo["quarters"]["2"]["4"],
        };

        List<string> match3 = new List<string>
        {
            cupInfo["quarters"]["3"]["5"],
            cupInfo["quarters"]["3"]["6"],
        };

        List<string> match4 = new List<string>
        {
            cupInfo["quarters"]["4"]["7"],
            cupInfo["quarters"]["4"]["8"],
        };

        cupInfo["quarters"]["1"]["winner"] = match1[UnityEngine.Random.Range(0, match1.Count)];
        cupInfo["quarters"]["2"]["winner"] = match2[UnityEngine.Random.Range(0, match2.Count)];
        cupInfo["quarters"]["3"]["winner"] = match3[UnityEngine.Random.Range(0, match3.Count)];
        cupInfo["quarters"]["4"]["winner"] = match4[UnityEngine.Random.Range(0, match4.Count)];

        Cup.Instance.round = "semis";

        Cup.Instance.cupInfo = cupInfo;
        Cup.Instance.SaveCup();
        Debug.Log("Simulated quarters!");
    }

    public List<CupFighter> GenerateParticipantsBasedOnQuarters()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> _cupInfo = Cup.Instance.cupInfo;
        List<CupFighter> _participants = Cup.Instance.participants;

        List<CupFighter> semisParticipants = new List<CupFighter>();
        int matchesNumber = 4;

        foreach(CupFighter participant in _participants)
        {
            for(int matchCounter = 1; matchCounter < matchesNumber + 1; matchCounter++)
            {
                if (participant.id == _cupInfo["quarters"][matchCounter.ToString()]["winner"])
                    semisParticipants.Add(participant);
            }
        }

        return semisParticipants;
    }
}
