using UnityEngine;
using System.Collections.Generic;
//Does everyone need to install Newtonsoft.Json (Install-Package Newtonsoft.Json) for this to work or is it installed in the project?
using Newtonsoft.Json;
using System.IO;

public static class SaveAndReadDataTest
{
    public static string path = @"D:\save.txt";
    public static void SaveData(List<object> data)
    {
        string json = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);

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
