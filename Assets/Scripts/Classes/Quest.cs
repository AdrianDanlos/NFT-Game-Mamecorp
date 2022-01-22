using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    /*
     * Quest type ideas:
     * 
     * - win x games
     * - play x turns
     * - play x mode
     * - reach x level (one time quests for early game - lots of rewards)
     * - play x cost cards
     * 
     * Seasonal/events quests (future)
    */

    private string _title;
    private string _description;
    private int _goal;
    private string _reward;

    public string title { get => _title; set => _title = value; }
    public string description { get => _description; set => _description = value; }
    private int goal { get => _goal; set => _goal = value; }
    public string reward { get => _reward; set => _reward = value; }

}
