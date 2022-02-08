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
        int eloChange = baseEloGain + eloPonderance;
        return isPlayerWinner ? eloChange : -eloChange;
    }
}