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
                {"trophies", "100"},
                {"specie", "Orc"},
            }
        },
        {
            "002",
            new Dictionary<string, string>{
                {"name", "Turges"},
                {"country", "ITA"},
                {"trophies", "100"},
                {"specie", "Ogre"},
            }
        },
        {
            "003",
            new Dictionary<string, string>{
                {"name", "adamqa"},
                {"country", "ESP"},
                {"trophies", "100"},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "004",
            new Dictionary<string, string>{
                {"name", "ellster16"},
                {"country", "DEU"},
                {"trophies", "100"},
                {"specie", "Golem1"},
            }
        },
        {
            "005",
            new Dictionary<string, string>{
                {"name", "thenameisG"},
                {"country", "CHN"},
                {"trophies", "100"},
                {"specie", "FallenAngel2"},
            }
        },
        {
            "006",
            new Dictionary<string, string>{
                {"name", "Yorphudi"},
                {"country", "PRT"},
                {"trophies", "100"},
                {"specie", "Goblin"},
            }
        },
        {
            "007",
            new Dictionary<string, string>{
                {"name", "emf44"},
                {"country", "KOR"},
                {"trophies", "100"},
                {"specie", "FallenAngel3"},
            }
        },
        {
            "008",
            new Dictionary<string, string>{
                {"name", "kirisunaKUN"},
                {"country", "FRA"},
                {"trophies", "100"},
                {"specie", "Goblin"},
            }
        },
        {
            "009",
            new Dictionary<string, string>{
                {"name", "inkkkk8"},
                {"country", "RUS"},
                {"trophies", "100"},
                {"specie", "Golem2"},
            }
        },
        {
            "010",
            new Dictionary<string, string>{
                {"name", "jeje6789"},
                {"country", "ESP"},
                {"trophies", "100"},
                {"specie", "Golem1"},
            }
        },
        {
            "011",
            new Dictionary<string, string>{
                {"name", "Crowcifer"},
                {"country", "fra"},
                {"trophies", "100"},
                {"specie", "Orc"},
            }
        },
        {
            "012",
            new Dictionary<string, string>{
                {"name", "IISilverIII"},
                {"country", "SWE"},
                {"trophies", "100"},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "013",
            new Dictionary<string, string>{
                {"name", "Stoner8008"},
                {"country", "ITA"},
                {"trophies", "100"},
                {"specie", "Golem3"},
            }
        },
        {
            "014",
            new Dictionary<string, string>{
                {"name", "EloiseJolie"},
                {"country", "FRA"},
                {"trophies", "100"},
                {"specie", "Goblin"},
            }
        },
        {
            "015",
            new Dictionary<string, string>{
                {"name", "huda khatib"},
                {"country", "TUR"},
                {"trophies", "100"},
                {"specie", "Ogre"},
            }
        },
    };

    public static void UpdateDB(int player)
    {
        switch (player)
        {
            case 001:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 002:
                PlayerPrefs.SetInt("player" + player, Random.Range(-10, 30));
                break;
            case 003:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 004:
                PlayerPrefs.SetInt("player" + player, Random.Range(20, 60));
                break;
            case 005:
                PlayerPrefs.SetInt("player" + player, Random.Range(-10, 30));
                break;
            case 006:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 007:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 008:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 009:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 010:
                PlayerPrefs.SetInt("player" + player, Random.Range(20, 60));
                break;
            case 011:
                PlayerPrefs.SetInt("player" + player, Random.Range(-10, 10));
                break;
            case 012:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 013:
                PlayerPrefs.SetInt("player" + player, Random.Range(50, 100));
                break;
            case 014:
                PlayerPrefs.SetInt("player" + player, Random.Range(-10, 10));
                break;
            case 015:
                PlayerPrefs.SetInt("player" + player, Random.Range(20, 60));
                break;
        }

        PlayerPrefs.Save();
    }
}
