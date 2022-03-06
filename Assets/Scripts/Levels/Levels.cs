using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
public static class Levels
{
    //This constant is used to calculate the total xp of each level. Formula: Level x LevelMultiplier.
    const int LevelMultiplier = 15;
    public static int MaxXpOfCurrentLevel(int playerLevel)
    {
        return playerLevel * LevelMultiplier;
    }

    public static int MinXpOfCurrentLevel(int playerLevel)
    {
        return playerLevel - 1 * LevelMultiplier;
    }
    public static Func<bool, int> GetXpGain = isPlayerWinner => isPlayerWinner ? 20 : 10;

    public static void SetLevel(Fighter player)
    {
        player.level++;
    }

    
    public static bool IsLevelUp(int playerCurrentLevel, int playerUpdatedExperience)
    {
        return MaxXpOfCurrentLevel(playerCurrentLevel) <= playerUpdatedExperience;
    }

    public static void ResetExperience(Fighter player)
    {
        player.experiencePoints -= MaxXpOfCurrentLevel(player.level);
    }

    public static void UpgradeStats(Fighter player)
    {
        foreach (KeyValuePair<SpeciesNames, Dictionary<string, float>> species in Species.statsPerLevel)
        {
            //species -> [Monster, Dictionary<string, float>] ...
            var isCurrentSpecies = false;

            foreach (PropertyInfo prop in species.GetType().GetProperties())
            {
                //prop -> Iteration 1: Monster, Iteration 2: Dictionary<string, float>
                if (isCurrentSpecies)
                {
                    SpeciesNames speciesEnumMember = (SpeciesNames)Enum.Parse(typeof(SpeciesNames), player.species); //Monster, Robot, Alien
                    player.hp += Species.statsPerLevel[speciesEnumMember]["hp"];
                    player.damage += Species.statsPerLevel[speciesEnumMember]["damage"];
                    player.speed += Species.statsPerLevel[speciesEnumMember]["speed"];
                }

                if (prop.GetValue(species, null).ToString() == player.species) isCurrentSpecies = true;
            }
        }
    }
}