using System.Collections.Generic;
using System.Collections.Specialized;
using System;

public static class MatchMaking
{
    private static int baseEloGain = 15;
    public static void GenerateBotData(Fighter player, Fighter bot)
    {
        string botName = MatchMaking.FetchBotRandomName();
        int botLevel = MatchMaking.GenerateBotLevel(player.level);
        Combat.botElo = MatchMaking.GenerateBotElo(User.Instance.elo);

        List<Skill> botSkills = new List<Skill>();

        //ADD ALL SKILLS
        foreach (OrderedDictionary skill in SkillCollection.skills)
        {
            Skill skillInstance = new Skill(skill["name"].ToString(), skill["description"].ToString(),
                skill["skillRarity"].ToString(), skill["category"].ToString(), skill["icon"].ToString());

            botSkills.Add(skillInstance);
        }

        SpeciesNames randomSpecies = GetRandomSpecies();

        Dictionary<string, float> botStats = GenerateBotRandomStats(randomSpecies);

        bot.FighterConstructor(botName, botStats["hp"], botStats["damage"], botStats["speed"],
            randomSpecies.ToString(), randomSpecies.ToString(), botLevel, 0, botSkills);

        //FIXME: We should remove the skin concept from the fighters and use the species name for the skin.
    }
    private static Dictionary<string, float> GenerateBotRandomStats(SpeciesNames randomSpecies)
    {
        float hp = Species.defaultStats[randomSpecies]["hp"] + (Species.statsPerLevel[randomSpecies]["hp"] * Combat.player.level);
        float damage = Species.defaultStats[randomSpecies]["damage"] + (Species.statsPerLevel[randomSpecies]["damage"] * Combat.player.level);
        float speed = Species.defaultStats[randomSpecies]["speed"] + (Species.statsPerLevel[randomSpecies]["speed"] * Combat.player.level);

        return new Dictionary<string, float>
        {
            {"hp", hp},
            {"damage", damage},
            {"speed", speed},
        };
    }
    private static SpeciesNames GetRandomSpecies()
    {
        System.Random random = new System.Random();
        Array species = Enum.GetValues(typeof(SpeciesNames));
        return (SpeciesNames)species.GetValue(random.Next(species.Length));
    }
    private static string FetchBotRandomName()
    {
        return RandomNameGenerator.GenerateRandomName();
    }

    private static int GenerateBotElo(int playerElo)
    {
        int botElo = UnityEngine.Random.Range(playerElo - 50, playerElo + 50);
        return botElo >= 0 ? botElo : 0;
    }
    private static int GenerateBotLevel(int playerLevel)
    {
        return playerLevel > 1 ? UnityEngine.Random.Range(playerLevel - 1, playerLevel + 2) : playerLevel;
    }

    public static int CalculateEloChange(int playerElo, int botElo, bool isPlayerWinner)
    {
        int eloDifference = botElo - playerElo;
        int eloPonderance = eloDifference / 10;
        int absoluteEloChange = baseEloGain + eloPonderance;
        int modifierToRankUpOverTime = 2;
        int eloChange = isPlayerWinner ? absoluteEloChange : -absoluteEloChange + modifierToRankUpOverTime;
        if (playerElo + eloChange < 0) return -playerElo;
        return eloChange;
    }
}