using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

public class Cup 
{
    private void SaveCup()
    {
        JsonDataManager.SaveData(JObject.FromObject(this), JsonDataManager.UserFileName);
    }
}
