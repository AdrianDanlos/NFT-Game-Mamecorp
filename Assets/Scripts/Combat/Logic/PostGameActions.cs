using UnityEngine;
using System;

public class PostGameActions
{
    public static void SetElo(int eloChange)
    {
        User.Instance.elo += eloChange;
    }

    //Functional Pattern. Func<ParameterType, ReturnType>
    public static Func<Fighter, bool> HasPlayerWon = player => player.hp > 0 ? true : false;
    //Actions are used in the same way as Func but with no return.
    public static Action<float> ResetPlayerHp = (playerMaxHp) => Combat.player.hp = playerMaxHp;

    public static void SetExperience(Fighter player, bool isPlayerWinner)
    {
        player.experiencePoints += Levels.GetXpGain(isPlayerWinner);
    }
    public static void SetLevelUpSideEffects(Fighter player)
    {
        Levels.ResetExperience(player);
        Levels.UpgradeStats(player);
        Levels.SetLevel(player);
    }

    public static void SetWinLoseCounter(bool isPlayerWinner)
    {
        if (isPlayerWinner) User.Instance.wins++;
        else User.Instance.loses++;
    }

    public static void SetCurrencies(bool isPlayerWinner, bool isLevelUp)
    {
        User.Instance.gold += isPlayerWinner ? 40 : 10;
        User.Instance.gems += isLevelUp && Probabilities.IsHappening(50) ? 20 : 0;
    }
    public static Action<Fighter> Save = (player) => player.SaveFighter();
}