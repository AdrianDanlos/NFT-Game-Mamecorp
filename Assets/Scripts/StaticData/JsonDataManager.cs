//Does everyone need to install Newtonsoft.Json (Install-Package Newtonsoft.Json) for this to work or is it installed in the project?
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public static class JsonDataManager
{
    public const string SAVE_PATH = @"D:\GameData";
    public const string USER_FILE_NAME = "user";
    public const string FIGHTER_FILE_NAME = "fighter";
    public static void SaveData(JObject data, string fileName)
    {
        System.IO.Directory.CreateDirectory(SAVE_PATH);
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
        return $"{SAVE_PATH}\\{fileName}.txt";
    }

    //We need to create a fighter class that is not monobehaviour to be able to serialize and save the data into the JSON file.
    public static SerializableFighter CreateSerializableFighterInstance(Fighter fighter)
    {
        return new SerializableFighter(fighter);
    }
}
