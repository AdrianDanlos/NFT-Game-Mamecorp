using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserFactory
{
    public static void CreateUserInstance(string userIcon, int energy, int wins = 0, int loses = 0, int elo = 0, int gold = 0, int gems = 0)
    {
        User user = User.Instance;
        user.SetUserValues(userIcon, wins, loses, elo, gold, gems, energy);
        user.EnableSave();
    }
}
