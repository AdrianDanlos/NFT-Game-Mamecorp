using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardDB
{
    const int INITIAL_TROPHIES_MIN = 900;
    const int INITIAL_TROPHIES_MAX = 1200;

    const int LOW_TROPHY_GAINS_MIN = 30;
    const int LOW_TROPHY_GAINS_MAX = 40;

    const int MEDIUM_TROPHY_GAINS_MIN = 40;
    const int MEDIUM_TROPHY_GAINS_MAX = 55;

    const int BIG_TROPHY_GAINS_MIN = 50;
    const int BIG_TROPHY_GAINS_MAX = 60;


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
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Orc"},
            }
        },
        {
            "2",
            new Dictionary<string, string>{
                {"name", "Turges"},
                {"country", "ITA"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Ogre"},
            }
        },
        {
            "3",
            new Dictionary<string, string>{
                {"name", "adamqa"},
                {"country", "ESP"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "4",
            new Dictionary<string, string>{
                {"name", "ellster16"},
                {"country", "DEU"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Golem1"},
            }
        },
        {
            "5",
            new Dictionary<string, string>{
                {"name", "thenameisG"},
                {"country", "CHN"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "FallenAngel2"},
            }
        },
        {
            "6",
            new Dictionary<string, string>{
                {"name", "Yorphudi"},
                {"country", "PRT"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Goblin"},
            }
        },
        {
            "7",
            new Dictionary<string, string>{
                {"name", "emf44"},
                {"country", "KOR"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "FallenAngel3"},
            }
        },
        {
            "8",
            new Dictionary<string, string>{
                {"name", "kirisunaKUN"},
                {"country", "FRA"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Goblin"},
            }
        },
        {
            "9",
            new Dictionary<string, string>{
                {"name", "inkkkk8"},
                {"country", "RUS"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Golem2"},
            }
        },
        {
            "10",
            new Dictionary<string, string>{
                {"name", "jeje6789"},
                {"country", "ESP"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Golem1"},
            }
        },
        {
            "11",
            new Dictionary<string, string>{
                {"name", "Crowcifer"},
                {"country", "fra"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Orc"},
            }
        },
        {
            "12",
            new Dictionary<string, string>{
                {"name", "IISilverIII"},
                {"country", "SWE"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "FallenAngel1"},
            }
        },
        {
            "13",
            new Dictionary<string, string>{
                {"name", "Stoner8008"},
                {"country", "ITA"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Golem3"},
            }
        },
        {
            "14",
            new Dictionary<string, string>{
                {"name", "EloiseJolie"},
                {"country", "FRA"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Goblin"},
            }
        },
        {
            "15",
            new Dictionary<string, string>{
                {"name", "huda khatib"},
                {"country", "TUR"},
                {"trophies", GenerateInitialTrophies().ToString()},
                {"specie", "Ogre"},
            }
        },
    };

    public static void UpdateDB(int player)
    {
        switch (player)
        {
            case 1:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetBigTrophyGains());
                break;
            case 2:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetLowTrophyGains());
                break;
            case 3:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetBigTrophyGains());
                break;
            case 4:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetBigTrophyGains());
                break;
            case 5:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetLowTrophyGains());
                break;
            case 6:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetBigTrophyGains());
                break;
            case 7:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetMediumTrophyGains());
                break;
            case 8:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetBigTrophyGains());
                break;
            case 9:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetBigTrophyGains());
                break;
            case 10:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetLowTrophyGains());
                break;
            case 11:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetMediumTrophyGains());
                break;
            case 12:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetLowTrophyGains());
                break;
            case 13:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetLowTrophyGains());
                break;
            case 14:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetMediumTrophyGains());
                break;
            case 15:
                PlayerPrefs.SetInt("player" + player, PlayerPrefs.GetInt("player" + player) + GetLowTrophyGains());
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
        PlayerPrefs.SetInt("player1", PlayerPrefs.GetInt("player1") + GetBigTrophyGains());
        PlayerPrefs.SetInt("player2", PlayerPrefs.GetInt("player2") + GetLowTrophyGains());
        PlayerPrefs.SetInt("player3", PlayerPrefs.GetInt("player3") + GetBigTrophyGains());
        PlayerPrefs.SetInt("player4", PlayerPrefs.GetInt("player4") + GetBigTrophyGains());
        PlayerPrefs.SetInt("player5", PlayerPrefs.GetInt("player5") + GetLowTrophyGains());
        PlayerPrefs.SetInt("player6", PlayerPrefs.GetInt("player6") + GetBigTrophyGains());
        PlayerPrefs.SetInt("player7", PlayerPrefs.GetInt("player7") + GetMediumTrophyGains());
        PlayerPrefs.SetInt("player8", PlayerPrefs.GetInt("player8") + GetBigTrophyGains());
        PlayerPrefs.SetInt("player9", PlayerPrefs.GetInt("player9") + GetBigTrophyGains());
        PlayerPrefs.SetInt("player10", PlayerPrefs.GetInt("player10") + GetLowTrophyGains());
        PlayerPrefs.SetInt("player11", PlayerPrefs.GetInt("player11") + GetMediumTrophyGains());
        PlayerPrefs.SetInt("player12", PlayerPrefs.GetInt("player12") + GetLowTrophyGains());
        PlayerPrefs.SetInt("player13", PlayerPrefs.GetInt("player13") + GetLowTrophyGains());
        PlayerPrefs.SetInt("player14", PlayerPrefs.GetInt("player14") + GetMediumTrophyGains());
        PlayerPrefs.SetInt("player15", PlayerPrefs.GetInt("player15") + GetLowTrophyGains());

        PlayerPrefs.Save();
    }

    private static int GetLowTrophyGains()
    {
        return Random.Range(LOW_TROPHY_GAINS_MIN, LOW_TROPHY_GAINS_MAX);
    }

    private static int GetMediumTrophyGains()
    {
        return Random.Range(MEDIUM_TROPHY_GAINS_MIN, MEDIUM_TROPHY_GAINS_MAX);
    }

    private static int GetBigTrophyGains()
    {
        return Random.Range(BIG_TROPHY_GAINS_MIN, BIG_TROPHY_GAINS_MAX);
    }

    private static int GenerateInitialTrophies()
    {
        return Random.Range(INITIAL_TROPHIES_MIN, INITIAL_TROPHIES_MAX);
    }
}
