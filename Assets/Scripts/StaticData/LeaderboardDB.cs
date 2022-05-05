using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardDB
{
    public static readonly Dictionary<string, Dictionary<string, string>> defaultStats =
    new Dictionary<string, Dictionary<string, string>>
    {
        {
            "player1", 
            new Dictionary<string, string>{
                {"country", "f"},
                {"hp", "10"},
                {"hp", "10"},
            }
        }
    };
}
