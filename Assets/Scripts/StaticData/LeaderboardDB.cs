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
            "1", 
            new Dictionary<string, string>{
                {"name", "monstersarius"},
                {"country", "GRE"},
                {"trophies", "100"},
                {"specie", "Orc"},
            }
        },
        {
            "2",
            new Dictionary<string, string>{
                {"name", "Turges"},
                {"country", "ITA"},
                {"trophies", "100"},
                {"specie", "Ogre"},
            }
        },
        {
            "3",
            new Dictionary<string, string>{
                {"name", "adamqa"},
                {"country", "ESP"},
                {"trophies", "100"},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "4",
            new Dictionary<string, string>{
                {"name", "ellster16"},
                {"country", "DEU"},
                {"trophies", "100"},
                {"specie", "Golem1"},
            }
        },
        {
            "5",
            new Dictionary<string, string>{
                {"name", "thenameisG"},
                {"country", "CHN"},
                {"trophies", "100"},
                {"specie", "FallenAngel2"},
            }
        },
        {
            "6",
            new Dictionary<string, string>{
                {"name", "Yorphudi"},
                {"country", "PRT"},
                {"trophies", "100"},
                {"specie", "Goblin"},
            }
        },
        {
            "7",
            new Dictionary<string, string>{
                {"name", "emf44"},
                {"country", "KOR"},
                {"trophies", "100"},
                {"specie", "FallenAngel3"},
            }
        },
        {
            "8",
            new Dictionary<string, string>{
                {"name", "kirisunaKUN"},
                {"country", "FRA"},
                {"trophies", "100"},
                {"specie", "Goblin"},
            }
        },
        {
            "9",
            new Dictionary<string, string>{
                {"name", "inkkkk8"},
                {"country", "RUS"},
                {"trophies", "100"},
                {"specie", "Golem2"},
            }
        },
        {
            "10",
            new Dictionary<string, string>{
                {"name", "jeje6789"},
                {"country", "ESP"},
                {"trophies", "100"},
                {"specie", "Golem1"},
            }
        },
        {
            "11",
            new Dictionary<string, string>{
                {"name", "Crowcifer"},
                {"country", "fra"},
                {"trophies", "100"},
                {"specie", "Orc"},
            }
        },
        {
            "12",
            new Dictionary<string, string>{
                {"name", "IISilverIII"},
                {"country", "SWE"},
                {"trophies", "100"},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "13",
            new Dictionary<string, string>{
                {"name", "Stoner8008"},
                {"country", "ITA"},
                {"trophies", "100"},
                {"specie", "Golem3"},
            }
        },
        {
            "14",
            new Dictionary<string, string>{
                {"name", "EloiseJolie"},
                {"country", "FRA"},
                {"trophies", "100"},
                {"specie", "Goblin"},
            }
        },
        {
            "15",
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
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 002:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(0, 30));
                break;
            case 003:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 004:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(20, 60));
                break;
            case 005:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(0, 30));
                break;
            case 006:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 007:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 008:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 009:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 010:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(20, 60));
                break;
            case 011:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(0, 10));
                break;
            case 012:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 013:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(50, 100));
                break;
            case 014:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(0, 10));
                break;
            case 015:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + Random.Range(20, 60));
                break;
        }

        PlayerPrefs.Save();
    }

    public static int GetUserTrophies(string id)
    {
        return PlayerPrefs.GetInt("player" + id);
    }

    public static void UpdateDB()
    {
        PlayerPrefs.SetInt("player1", PlayerPrefs.GetInt("player1") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player2", PlayerPrefs.GetInt("player2") + Random.Range(-10, 30));
        PlayerPrefs.SetInt("player3", PlayerPrefs.GetInt("player3") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player4", PlayerPrefs.GetInt("player4") + Random.Range(20, 60));
        PlayerPrefs.SetInt("player5", PlayerPrefs.GetInt("player5") + Random.Range(-10, 30));
        PlayerPrefs.SetInt("player6", PlayerPrefs.GetInt("player6") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player7", PlayerPrefs.GetInt("player7") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player8", PlayerPrefs.GetInt("player8") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player9", PlayerPrefs.GetInt("player9") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player10", PlayerPrefs.GetInt("player10") + Random.Range(20, 60));
        PlayerPrefs.SetInt("player11", PlayerPrefs.GetInt("player11") + Random.Range(-10, 10));
        PlayerPrefs.SetInt("player12", PlayerPrefs.GetInt("player12") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player13", PlayerPrefs.GetInt("player13") + Random.Range(50, 100));
        PlayerPrefs.SetInt("player14", PlayerPrefs.GetInt("player14") + Random.Range(-10, 10));
        PlayerPrefs.SetInt("player15", PlayerPrefs.GetInt("player15") + Random.Range(20, 60));

        PlayerPrefs.Save();
    }
}
