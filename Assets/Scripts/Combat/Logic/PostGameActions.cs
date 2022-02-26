using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
public class PostGameActions
{
    public static void SetElo(int eloChange)
    {
        User.Instance.elo += eloChange;
    }

    public static void EnableResults(Canvas results)
    {
        results.enabled = true;
    }

    //Functional Pattern. Func<ParameterType, ReturnType>
    public static Func<Fighter, bool> HasPlayerWon = player => player.hp > 0 ? true : false;
    //Actions are used in the same way as Func but with no return.
    public static Action<float> ResetPlayerHp = (playerMaxHp) => Combat.player.hp = playerMaxHp;
    public static void HideLoserFighter()
    {
        if (PostGameActions.HasPlayerWon(Combat.player)) Combat.botGameObject.SetActive(false);
        else Combat.playerGameObject.SetActive(false);
    }

    public static void SetExperience(Fighter player, bool isPlayerWinner)
    {
        player.experiencePoints += Levels.GetXpGain(isPlayerWinner);
        if (Levels.IsLevelUp(player))
        {
            SetLevel(player);
            UpgradeStats(player);
        }
    }

    private static void SetLevel(Fighter player)
    {
        player.level++;
    }

    private static void UpgradeStats(Fighter player)
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

    public static Action<Fighter> Save = (player) => player.SaveFighter();
}