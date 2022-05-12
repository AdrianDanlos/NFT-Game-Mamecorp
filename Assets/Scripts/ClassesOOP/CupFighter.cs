using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupFighter
{
    // Use this class to generate cup opponents in combat
    // and save them in cup file
    private string _id;
    private string _fighterName;
    private string _species;
    private int _elo;

    public string id
    {
        get => _id; set
        {
            _id = value;
        }
    }
    public string fighterName
    {
        get => _fighterName; set
        {
            _fighterName = value;
        }
    }
    public string species
    {
        get => _species; set
        {
            _species = value;
        }
    }
    public int elo
    {
        get => _elo; set
        {
            _elo = value;
        }
    }
}
