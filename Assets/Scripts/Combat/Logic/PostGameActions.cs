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

    public static void SetCurrencies(int goldReward, int gemsReward)
    {
        User.Instance.gold += goldReward;
        User.Instance.gems += gemsReward;
    }
    public static Action<Fighter> Save = (player) => player.SaveFighter();

    public static int GoldReward(bool isPlayerWinner)
    {
        return isPlayerWinner ? 20 : 10;
    }

    public static int GemsReward()
    {
        return Probabilities.IsHappening(10) ? UnityEngine.Random.Range(20, 40) : 0;
    }
}