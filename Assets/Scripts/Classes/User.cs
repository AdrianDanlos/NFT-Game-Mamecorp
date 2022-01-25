//This is a singleton class -> https://en.wikipedia.org/wiki/Singleton_pattern
//By making it a singleton we achieve 2 things:
//1. We ensure there is only one instance of the user
//2. We can make the instance static and therefore access it from anywhere in our game
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

    public void SetUserValues(string userNameParam, int winsParam, int losesParam, int eloParam)
    {
        userName = userNameParam;
        wins = winsParam;
        loses = losesParam;
        elo = eloParam;
    }
}
