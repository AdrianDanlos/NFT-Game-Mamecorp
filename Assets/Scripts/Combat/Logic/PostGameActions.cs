using UnityEngine;
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

    public static bool HasPlayerWon(Fighter player)
    {
        return player.hp > 0 ? true : false;
    }
}