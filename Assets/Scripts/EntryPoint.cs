using System.IO;
using UnityEngine;
using System.Collections.Generic;


public static class EntryPoint
{
    public static void TestEntryPoint()
    {
        //FIXME: Move this to start/awake + change name + attach this to entrypoint gameobject
        if (File.Exists(SaveAndReadDataTest.path))
        {
            //TODO: We should save the gameData globally in the application (store, global file...) so it can be accesed anytime
            var gameData = SaveAndReadDataTest.ReadData();
            Debug.Log(gameData);
        }
        else
        {
            User newUser = CreateUser();
            Fighter newFighter = CreateFighter();
            List<object> data = new List<object>(){ newUser, newFighter };
            SaveAndReadDataTest.SaveData(data);
        }        
    }

    public static User CreateUser()
    {
        //TODO: Pedir nombre al usuario en escena
        return new User("Berthold", 10, 10, 0);
    }

    public static Fighter CreateFighter()
    {
        //TODO: Pedir nombre del fighter al usuario en escena
        //return new Fighter("Eren", 10, 1, 3, "Fire", 1, 10, new List<Card>());
        return null;
    }
}