using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInfoManager : MonoBehaviour
{
    public void CreateCupFile()
    {
        string cupName = cupNames[Random.Range(0, cupNames.Count)]; 
        
        CupFactory.CreateCupInstance(cupName, userIcon, PlayerUtils.maxEnergy);
        JObject cup = JObject.FromObject(User.Instance);
        JsonDataManager.SaveData(user, JsonDataManager.UserFileName);
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
