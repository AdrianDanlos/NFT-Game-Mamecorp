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
    private bool _saveEnabled = false;
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
            _wins = value;
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
    public bool saveEnabled
    {
        get => _saveEnabled; set
        {
            _saveEnabled = value;
        }
    }
    public void SetUserValues(string userName, int wins, int loses, int elo)
    {
        this.userName = userName;
        this.wins = wins;
        this.loses = loses;
        this.elo = elo;
    }

    // We call it once the user has been instantiated.
    // Otherwise we would be calling the save function for each attribute that is being assigned in the constructor.
    public void EnableSave()
    {
        this.saveEnabled = true;
    }

    private void SaveUser()
    {
        if (saveEnabled == true)
        {
            JsonDataManager.SaveData(JObject.FromObject(this), JsonDataManager.UserFileName);
        }
    }
}
