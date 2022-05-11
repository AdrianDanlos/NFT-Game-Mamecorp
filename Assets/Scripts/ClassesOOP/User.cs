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
    private string _userIcon;
    private int _wins;
    private int _loses;
    private int _elo;
    private int _gold;
    private int _gems;
    private int _energy;
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
    public string userIcon
    {
        get => _userIcon;
        set
        {
            _userIcon = value;
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
    public int gold
    {
        get => _gold;
        set
        {
            _gold = value;
            SaveUser();
        }
    }

    public int gems
    {
        get => _gems;
        set
        {
            _gems = value;
            SaveUser();
        }
    }

    public int energy
    {
        get => _energy;
        set
        {
            _energy = value;
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
    public void SetUserValues(string userName, string userIcon, int wins, int loses, int elo, int gold, int gems, int energy)
    {
        this.userName = userName;
        this.userIcon = userIcon;
        this.wins = wins;
        this.loses = loses;
        this.elo = elo;
        this.gold = gold;
        this.gems = gems;
        this.energy = energy;
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
