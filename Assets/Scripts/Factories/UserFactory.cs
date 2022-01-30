using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserFactory
{
    public static void CreateUserInstance(string userName, int wins = 0, int loses = 0, int elo = 0)
    {
        User user = User.Instance;
        user.SetUserValues(userName, wins, loses, elo);
    }
}
