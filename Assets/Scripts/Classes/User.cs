using UnityEngine;

public class User
{
    private string _userName;
    private int _wins;
    private int _loses;
    private int _elo;
    public string userName { get => _userName; set => _userName = value; }
    public int wins { get => _wins; set => _wins = value; }
    public int loses { get => _loses; set => _loses = value; }
    public int elo { get => _elo; set => _elo = value; }

    public User(string userName, int wins, int loses, int elo)
    {
        this.userName = userName;
        this.wins = wins;
        this.loses = loses;
        this.elo = elo;
    }
}
