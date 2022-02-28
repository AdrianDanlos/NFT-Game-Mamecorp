using UnityEngine;
using System;

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
    }
    public static void SetLevelUpSideEffects(Fighter player)
    {
        Levels.ResetExperience(player);
        Levels.UpgradeStats(player);
        Levels.SetLevel(player);
    }
    public static Action<Fighter> Save = (player) => player.SaveFighter();
}