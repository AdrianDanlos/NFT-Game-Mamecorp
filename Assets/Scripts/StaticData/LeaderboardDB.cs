using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardDB
{
    public enum Flag
    {
        CHN,
        DEU,
        ENG,
        ESP,
        FRA,
        GRE,
        INA,
        ITA,
        JPN,
        KOR,
        POL,
        PRA,
        PRT,
        ROU,
        RUS,
        SWE,
        THA,
        TUR,
        TWN,
        UKR
    }

    public static readonly Dictionary<string, Dictionary<string, string>> players =
    new Dictionary<string, Dictionary<string, string>>
    {
        {
            "001", 
            new Dictionary<string, string>{
                {"name", "monstersarius"},
                {"country", "GRE"},
                {"trophies", "0"},
                {"specie", "Orc"},
            }
        },
        {
            "002",
            new Dictionary<string, string>{
                {"name", "Turges"},
                {"country", "ITA"},
                {"trophies", "0"},
                {"specie", "Ogre"},
            }
        },
        {
            "003",
            new Dictionary<string, string>{
                {"name", "adamqa"},
                {"country", "ESP"},
                {"trophies", "0"},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "004",
            new Dictionary<string, string>{
                {"name", "ellster16"},
                {"country", "DEU"},
                {"trophies", "0"},
                {"specie", "Golem1"},
            }
        },
        {
            "005",
            new Dictionary<string, string>{
                {"name", "thenameisG"},
                {"country", "CHN"},
                {"trophies", "0"},
                {"specie", "FallenAngel2"},
            }
        },
        {
            "006",
            new Dictionary<string, string>{
                {"name", "Yorphudi"},
                {"country", "PRT"},
                {"trophies", "0"},
                {"specie", "Goblin"},
            }
        },
        {
            "007",
            new Dictionary<string, string>{
                {"name", "emf44"},
                {"country", "KOR"},
                {"trophies", "0"},
                {"specie", "FallenAngel3"},
            }
        },
        {
            "008",
            new Dictionary<string, string>{
                {"name", "kirisunaKUN"},
                {"country", "FRA"},
                {"trophies", "0"},
                {"specie", "Goblin"},
            }
        },
        {
            "009",
            new Dictionary<string, string>{
                {"name", "inkkkk8"},
                {"country", "RUS"},
                {"trophies", "0"},
                {"specie", "Golem2"},
            }
        },
        {
            "010",
            new Dictionary<string, string>{
                {"name", "jeje6789"},
                {"country", "ESP"},
                {"trophies", "0"},
                {"specie", "Golem1"},
            }
        },
        {
            "011",
            new Dictionary<string, string>{
                {"name", "Crowcifer"},
                {"country", "fra"},
                {"trophies", "0"},
                {"specie", "Orc"},
            }
        },
        {
            "012",
            new Dictionary<string, string>{
                {"name", "IISilverIII"},
                {"country", "SWE"},
                {"trophies", "0"},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "013",
            new Dictionary<string, string>{
                {"name", "Stoner8008"},
                {"country", "ITA"},
                {"trophies", "0"},
                {"specie", "Golem3"},
            }
        },
        {
            "014",
            new Dictionary<string, string>{
                {"name", "EloiseJolie"},
                {"country", "FRA"},
                {"trophies", "0"},
                {"specie", "Goblin"},
            }
        },
        {
            "015",
            new Dictionary<string, string>{
                {"name", "huda khatib"},
                {"country", "TUR"},
                {"trophies", "0"},
                {"specie", "Ogre"},
            }
        },
    };

    public static void UpdateDB()
    {
        PlayerPrefs.SetInt("player001", Random.Range(50, 100));
        PlayerPrefs.SetInt("player002", Random.Range(-10, 30));
        PlayerPrefs.SetInt("player003", Random.Range(50, 100));
        PlayerPrefs.SetInt("player004", Random.Range(20, 60));
        PlayerPrefs.SetInt("player005", Random.Range(50, 100));
        PlayerPrefs.SetInt("player006", Random.Range(-10, 30));
        PlayerPrefs.SetInt("player007", Random.Range(20, 60));
        PlayerPrefs.SetInt("player008", Random.Range(50, 100));
        PlayerPrefs.SetInt("player009", Random.Range(50, 100));
        PlayerPrefs.SetInt("player010", Random.Range(20, 60));
        PlayerPrefs.SetInt("player011", Random.Range(-10, 10));
        PlayerPrefs.SetInt("player012", Random.Range(50, 100));
        PlayerPrefs.SetInt("player013", Random.Range(50, 100));
        PlayerPrefs.SetInt("player014", Random.Range(-10, 10));
        PlayerPrefs.SetInt("player015", Random.Range(20, 60));
        PlayerPrefs.Save();
    }
}
