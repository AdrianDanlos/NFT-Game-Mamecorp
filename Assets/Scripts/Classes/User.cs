//This is a singleton class -> https://en.wikipedia.org/wiki/Singleton_pattern
//By making it a singleton we achieve 2 things:
//1. We ensure there is only one instance of the user
//2. We can make the instance static and therefore access it from anywhere in our game
using Newtonsoft.Json.Linq;
public class User
{
    private static User instance = null;
    private User() { }

    public static User Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }
    }

    private string _userName;
    private int _wins;
    private int _loses;
    private int _elo;
    public string userName
    {
        get => _userName;
        set
        {
            _userName = value;
            SaveUser();
        }
    }
    public int wins
    {
        get => _wins;
        set
        {
            wins = value;
            SaveUser();
        }
    }
    public int loses
    {
        get => _loses;
        set
        {
            _loses = value;
            SaveUser();
        }
    }
    public int elo
    {
        get => _elo;
        set
        {
            _elo = value;
            SaveUser();
        }
    }

    public void SetUserValues(string userName, int wins, int loses, int elo)
    {
        this.userName = userName;
        this.wins = wins;
        this.loses = loses;
        this.elo = elo;
    }

    private void SaveUser()
    {
        JsonDataManager.SaveData(JObject.FromObject(this), JsonDataManager.USER_FILE_NAME);
    }
}
