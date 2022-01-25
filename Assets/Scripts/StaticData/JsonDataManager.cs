using UnityEngine;
//Does everyone need to install Newtonsoft.Json (Install-Package Newtonsoft.Json) for this to work or is it installed in the project?
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

public static class JsonDataManager
{
    public static string savePath = @"D:\GameData";
    public static void SaveData(JObject data, string fileName)
    {
        System.IO.Directory.CreateDirectory(savePath);
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(getFilePath(fileName), json);
        Debug.Log("SavedData");
        Debug.Log(json);
    }

    public static JObject ReadData(string fileName)
    {
        using (StreamReader r = new StreamReader(getFilePath(fileName)))
        {
            string json = r.ReadToEnd();
            return JObject.Parse(json);
        }
    }

    public static string getFilePath(string fileName){
        return $"{savePath}\\{fileName}.txt";
    }
}
