using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserFactory
{
    public static void CreateUserInstance(string userName, int wins = 0, int loses = 0, int elo = 0, int gold = 0, int gems = 0, int energy = 5)
    {
        User user = User.Instance;
        user.SetUserValues(userName, wins, loses, elo, gold, gems, energy);
        user.EnableSave();
    }
}
