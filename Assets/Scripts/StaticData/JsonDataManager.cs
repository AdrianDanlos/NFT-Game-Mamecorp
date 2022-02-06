using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public static class JsonDataManager
{
    private const string SavePath = @"D:\GameData";
    public const string UserFileName = "user";
    public const string FighterFileName = "fighter";
    public static void SaveData(JObject data, string fileName)
    {
        System.IO.Directory.CreateDirectory(SavePath);
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        System.IO.File.WriteAllText(getFilePath(fileName), json);
    }

    public static JObject ReadData(string fileName)
    {
        using (StreamReader r = new StreamReader(getFilePath(fileName)))
        {
            string json = r.ReadToEnd();
            return JObject.Parse(json);
        }
    }

    public static string getFilePath(string fileName)
    {
        return $"{SavePath}\\{fileName}.txt";
    }

    //We need to create a fighter class that is not monobehaviour to be able to serialize and save the data into the JSON file.
    public static SerializableFighter CreateSerializableFighterInstance(Fighter fighter)
    {
        return new SerializableFighter(fighter);
    }
}
