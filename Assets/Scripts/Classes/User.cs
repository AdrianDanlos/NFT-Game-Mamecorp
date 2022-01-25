public class User
{
    private static User instance = null;
    private User(){}

    public static User Instance
    {
        get{
            if(instance == null){
                instance = new User();
            }
            return instance;
        }
    }

    private string _userName;
    private int _wins;
    private int _loses;
    private int _elo;
    public string userName { get => _userName; set => _userName = value; }
    public int wins { get => _wins; set => _wins = value; }
    public int loses { get => _loses; set => _loses = value; }
    public int elo { get => _elo; set => _elo = value; }

    public void UserConstructor(string userNameParam, int winsParam, int losesParam, int eloParam)
    {
        userName = userNameParam;
        wins = winsParam;
        loses = losesParam;
        elo = eloParam;
    }
}
