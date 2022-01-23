using System.IO;
using UnityEngine;
public static class EntryPoint
{
    public static void TestEntryPoint()
    {
        //FIXME: Move this to start/awake + change name + attach this to entrypoint gameobject
        if (File.Exists(SaveAndReadDataTest.path))
        {
            var gameData = SaveAndReadDataTest.ReadData();
            Debug.Log(gameData);
        }
        else
        {
            CreateUser();
        }        
    }

    public static void CreateUser()
    {
        //TODO: Pedir nombre al usuario en escena
        User newUser = new User("Berthold", 10, 10, 0);
        SaveAndReadDataTest.SaveData((object)newUser);
    }
}