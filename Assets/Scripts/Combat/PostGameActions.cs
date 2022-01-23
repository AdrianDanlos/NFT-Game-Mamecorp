public class PostGameActions {
    public static void UpdateElo(User user, int playerElo, int botElo, bool hasPlayerWon){
        int eloChange = MatchMaking.CalculateEloChange(playerElo, botElo, hasPlayerWon);
        //TODO: Update user elo from the user object here + save it
    }
}