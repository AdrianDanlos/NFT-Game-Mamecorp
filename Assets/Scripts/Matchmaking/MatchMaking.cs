using UnityEngine;
public static class MatchMaking
{
    public static int baseEloGain = 15;
    public static string FetchBotRandomName()
    {
        return RandomNameGenerator.GenerateRandomName();
    }

    public static int GenerateBotElo(int playerElo)
    {
        int botElo = Random.Range(playerElo - 50, playerElo + 50);
        return botElo >= 0 ? botElo : 0;
    }

    public static int CalculateEloChange(int playerElo, int botElo, bool isPlayerWinner)
    {
        int eloDifference = botElo - playerElo;
        int eloPonderance = eloDifference / 10;
        int absoluteEloChange = baseEloGain + eloPonderance;
        int modifierToRankUpOverTime = 2;
        int eloChange = isPlayerWinner ? absoluteEloChange : -absoluteEloChange + modifierToRankUpOverTime;
        if(playerElo + eloChange < 0) return -playerElo;
        return eloChange;
    }
}