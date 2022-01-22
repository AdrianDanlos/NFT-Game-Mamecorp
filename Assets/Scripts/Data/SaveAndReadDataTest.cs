using UnityEngine;
using System.Collections.Generic;
//Does everyone need to install Newtonsoft.Json (Install-Package Newtonsoft.Json) for this to work or is it installed in the project?
using Newtonsoft.Json;
using System.IO;

public static class SaveAndReadDataTest
{
    public static void SaveData()
    {
        List<Card> _data = new List<Card>();
        _data.Add(new Card("cartita", 5, "te destruye", "epic", "neutral"));

        string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

        //write string to file
        System.IO.File.WriteAllText(@"D:\path.txt", json);
        Debug.Log(json);
    }

    public static void ReadData()
    {
        using (StreamReader r = new StreamReader(@"D:\path.txt"))
        {
            string json = r.ReadToEnd();
            List<Card> card = JsonConvert.DeserializeObject<List<Card>>(json);
            Debug.Log(card[0].cardName);
        }
    }
}
