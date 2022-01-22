using UnityEngine;
public static class MatchMaking
{
    public static int baseEloGain = 15;
    public static string FetchBotRandomName(){
        return RandomNameGenerator.GenerateRandomName();
    }

    public static int GenerateBotElo(int playerElo){
        return Random.Range(playerElo - 50, playerElo + 50);
    }

    private static int CalculateEloChange(int playerElo, int botElo, bool hasPlayerWon){
        int eloDifference = botElo - playerElo;
        int eloPonderance = eloDifference / 10;
        return baseEloGain + eloPonderance;
    }

    public static void UpdateEloAfterCombat(User user, int playerElo, int botElo, bool hasPlayerWon){
        int eloChange = CalculateEloChange(playerElo, botElo, hasPlayerWon);
        //TODO: Update user elo from the user object here + save it
    }
}