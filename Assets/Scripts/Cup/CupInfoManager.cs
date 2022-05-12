using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInfoManager : MonoBehaviour
{
    private void Awake()
    {
        if(Cup.Instance == null)
        {
            CreateCupFile();
        }
    }

    public void CreateCupFile()
    {
        string cupName = cupNames[Random.Range(0, cupNames.Count)];
        List<CupFighter> participants = new List<CupFighter>();
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        CupFactory.CreateCupInstance(cupName, participants, cupInfo);
        JObject cup = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(cup, JsonDataManager.CupFileName);
    }

    // cup names DB
    private static List<string> cupNames = new List<string>
    {
        "Earth",
        "Water",
        "Air",
        "Fire"
    };
}
