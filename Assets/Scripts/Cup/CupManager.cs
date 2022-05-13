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
                                { "winner" , ""}    // winner 1/2
                            }
                        },
                        { "2", new Dictionary<string, string>
                            {
                                { "3", "2"} ,
                                { "4", "3"} ,
                                { "winner" , ""}
                            }
                        },
                        { "3", new Dictionary<string, string>
                            {
                                { "5", "4"} ,
                                { "2", "5"} ,
                                { "winner" , ""}
                            }
                        },
                        { "4", new Dictionary<string, string>
                            {
                                { "1", "6"} ,
                                { "2", "7"} ,
                                { "winner" , ""}
                            }
                        },
                    }
                }
            };

        return cupInfo;
    }


    // TODO save rounds
}
