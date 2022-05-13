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
                                { "winner" , ""} ,  // winner id
                                { "loser" , ""}     // loser id
                            }
                        },
                        { "2", new Dictionary<string, string>
                            {
                                { "matchId", "2"} ,
                                { "3", "2"} ,
                                { "4", "3"} ,
                                { "winner" , ""} ,  
                                { "loser" , ""}
                            }
                        },
                        { "3", new Dictionary<string, string>
                            {
                                { "matchId", "3"} ,
                                { "5", "4"} ,
                                { "6", "5"} ,
                                { "winner" , ""} ,  
                                { "loser" , ""}
                            }
                        },
                        { "4", new Dictionary<string, string>
                            {
                                { "matchId", "4"} ,
                                { "7", "6"} ,
                                { "8", "7"} ,
                                { "winner" , ""} ,  
                                { "loser" , ""}
                            }
                        },
                    }
                }
            };

        return cupInfo;
    }

    public void SimulateQuarters()
    {
        int random;
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = Cup.Instance.cupInfo;
        
        // match lists
        List<string> match1 = new List<string>
        {
            // this doesn't need to be simulated - player
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

        // simulate matches
        random = UnityEngine.Random.Range(0, match1.Count);
        cupInfo["quarters"]["1"]["winner"] = match1[random];
        if (random == 0)
            cupInfo["quarters"]["1"]["loser"] = match1[1];
        else
            cupInfo["quarters"]["1"]["loser"] = match1[0];

        random = UnityEngine.Random.Range(0, match2.Count);
        cupInfo["quarters"]["2"]["winner"] = match2[random];
        if (random == 0)
            cupInfo["quarters"]["2"]["loser"] = match2[1];
        else
            cupInfo["quarters"]["2"]["loser"] = match2[0];

        random = UnityEngine.Random.Range(0, match3.Count);
        cupInfo["quarters"]["3"]["winner"] = match3[random];
        if (random == 0)
            cupInfo["quarters"]["3"]["loser"] = match3[1];
        else
            cupInfo["quarters"]["3"]["loser"] = match3[0];

        random = UnityEngine.Random.Range(0, match4.Count);
        cupInfo["quarters"]["4"]["winner"] = match4[random];
        if (random == 0)
            cupInfo["quarters"]["4"]["loser"] = match4[1];
        else
            cupInfo["quarters"]["4"]["loser"] = match4[0];

        // save results
        Cup.Instance.round = "semis";

        Cup.Instance.cupInfo = cupInfo;
        Cup.Instance.SaveCup();
        Debug.Log("Simulated quarters!");

        GenerateCupSemisInfo();
        Debug.Log("Generated semis bracket!");
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

    // call on combat end 
    private void GenerateCupSemisInfo()
    {
        Cup.Instance.cupInfo.Add(
            "semis", new Dictionary<string, Dictionary<string, string>>
            {
                { "5", new Dictionary<string, string>
                    {
                        { "matchId", "5"} ,
                        { "9", ""} ,
                        { "10", ""} ,
                        { "winner" , ""} ,
                        { "loser" , ""}
                    }
                },
                { "6", new Dictionary<string, string>
                    {
                        { "matchId", "6"} ,
                        { "11", ""} ,
                        { "12", ""} ,
                        { "winner" , ""} ,
                        { "loser" , ""}
                    }
                },
            });

        List<CupFighter> _participants = GenerateParticipantsBasedOnQuarters();
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = Cup.Instance.cupInfo;
        int participantsCounter = 0; 

        cupInfo["semis"]["5"]["9"] = _participants[participantsCounter].id;
        participantsCounter++;
        cupInfo["semis"]["5"]["10"] = _participants[participantsCounter].id;
        participantsCounter++;
        cupInfo["semis"]["6"]["11"] = _participants[participantsCounter].id;
        participantsCounter++;
        cupInfo["semis"]["6"]["12"] = _participants[participantsCounter].id;

        Cup.Instance.cupInfo = cupInfo;
        Cup.Instance.SaveCup();
    }

    public void SimulateSemis()
    {
        int random;
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = Cup.Instance.cupInfo;
        
        // match lists
        List<string> match5 = new List<string>
        {
            // this doesn't need to be simulated - player
            cupInfo["semis"]["5"]["9"],
            cupInfo["semis"]["5"]["10"],
        };

        List<string> match6 = new List<string>
        {
            cupInfo["semis"]["6"]["11"],
            cupInfo["semis"]["6"]["12"],
        };

        // simulate matches
        random = UnityEngine.Random.Range(0, match5.Count);
        cupInfo["semis"]["5"]["winner"] = match5[random];
        if (random == 0)
            cupInfo["semis"]["5"]["loser"] = match5[1];
        else
            cupInfo["semis"]["5"]["loser"] = match5[0];

        random = UnityEngine.Random.Range(0, match6.Count);
        cupInfo["semis"]["6"]["winner"] = match6[random];
        if (random == 0)
            cupInfo["semis"]["6"]["loser"] = match6[1];
        else
            cupInfo["semis"]["6"]["loser"] = match6[0];

        // save results
        Cup.Instance.round = "finals";

        Cup.Instance.cupInfo = cupInfo;
        Cup.Instance.SaveCup();
        Debug.Log("Simulated semis!");

        GenerateCupFinalsInfo();
        Debug.Log("Generated finals bracket!");
    }

    public List<CupFighter> GenerateParticipantsBasedOnSemis()
    {
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> _cupInfo = Cup.Instance.cupInfo;
        List<CupFighter> _participants = Cup.Instance.participants;

        List<CupFighter> semisParticipants = new List<CupFighter>();
        int matchesNumber = 2;
        matchesNumber += 5;

        foreach (CupFighter participant in _participants)
        {
            for (int matchCounter = 5; matchCounter < matchesNumber; matchCounter++)
            {
                if (participant.id == _cupInfo["semis"][matchCounter.ToString()]["winner"])
                    semisParticipants.Add(participant);
            }
        }

        return semisParticipants;
    }

    private void GenerateCupFinalsInfo()
    {
        Cup.Instance.cupInfo.Add(
            "finals", new Dictionary<string, Dictionary<string, string>>
            {
                { "7", new Dictionary<string, string>
                    {
                        { "matchId", "7"} ,
                        { "13", ""} ,
                        { "14", ""} ,
                        { "winner" , ""} ,
                        { "loser" , ""}
                    }
                },
            });

        List<CupFighter> _participants = GenerateParticipantsBasedOnSemis();
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = Cup.Instance.cupInfo;
        int participantsCounter = 0;

        cupInfo["finals"]["7"]["13"] = _participants[participantsCounter].id;
        participantsCounter++;
        cupInfo["finals"]["7"]["14"] = _participants[participantsCounter].id;

        Cup.Instance.cupInfo = cupInfo;
        Cup.Instance.SaveCup();
    }

    public void SimulateFinals()
    {
        int random;
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = Cup.Instance.cupInfo;
        
        // matches lists
        List<string> match7 = new List<string>
        {
            // this doesn't need to be simulated - player
            cupInfo["finals"]["7"]["13"],
            cupInfo["finals"]["7"]["14"],
        };

        // simulate match
        random = UnityEngine.Random.Range(0, match7.Count);
        cupInfo["finals"]["7"]["winner"] = match7[random];
        if (random == 0)
            cupInfo["finals"]["7"]["loser"] = match7[1];
        else
            cupInfo["finals"]["7"]["loser"] = match7[0];

        // save results
        Cup.Instance.round = "end";

        Cup.Instance.cupInfo = cupInfo;
        Cup.Instance.SaveCup();
        Debug.Log("Simulated finals!");
    }
}
