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
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        CupFactory.CreateCupInstance(cupName, round, participants, cupInfo);
        JObject cup = JObject.FromObject(Cup.Instance);
        JsonDataManager.SaveData(cup, JsonDataManager.CupFileName);
    }

    private List<CupFighter> GenerateParticipants()
    {
        Fighter player = PlayerUtils.FindInactiveFighter();

        // there will be 8 fighters per cup (7 + user)
        int id = 0;
        List<CupFighter> participants = new List<CupFighter>();

        CupFighter user = new CupFighter(id.ToString(), player.fighterName, player.species);

        Debug.Log(user.id + " " + user.fighterName + " " + user.species);

        participants.Add(user);

        return participants;
    }
}
