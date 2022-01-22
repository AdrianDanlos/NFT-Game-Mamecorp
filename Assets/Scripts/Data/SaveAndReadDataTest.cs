using UnityEngine;
using System.Collections.Generic;
//Does everyone need to install Newtonsoft.Json (Install-Package Newtonsoft.Json) for this to work or is it installed in the project?
using Newtonsoft.Json;
using System.IO;

public static class SaveAndReadDataTest
{
    public static string path = @"D:\save.txt";
    public static void SaveData(object data)
    {
        List<object> _data = new List<object>();
        _data.Add(data);

        string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

        //write string to file
        System.IO.File.WriteAllText(path, json);
        Debug.Log(json);
    }

    public static List<object> ReadData()
    {
        using (StreamReader r = new StreamReader(path))
        {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<List<object>>(json);
        }
    }
}
