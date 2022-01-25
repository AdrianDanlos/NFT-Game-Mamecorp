using System.IO;
using UnityEngine;
using System.Collections.Generic;


public static class EntryPoint
{
    public static void ApplicationStart()
    {
        //FIXME: Move this to start/awake + change name + attach this to entrypoint gameobject
        if (File.Exists(SaveAndReadDataTest.path))
        {
            var gameData = SaveAndReadDataTest.ReadData();
            //TODO: Save user in static or singleton // Save fighter on gameobject
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
        User user = User.Instance;
        user.UserConstructor("Berthold", 10, 10, 0);
        Debug.Log(user.userName);
        return user;
    }

    public static Fighter CreateFighter()
    {
        //TODO: Pedir nombre del fighter al usuario en escena
        //return new Fighter("Eren", 10, 1, 3, "Fire", 1, 10, new List<Card>());
        return null;
    }
}