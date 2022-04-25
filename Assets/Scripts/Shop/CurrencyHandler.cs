using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
    public static CurrencyHandler instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddGems(int gems)
    {
        User.Instance.gems += gems;
    }

    public void AddGold(int gold)
    {
        User.Instance.gold += gold;
    }

    public void SubstractGems(int gems)
    {
        User.Instance.gems -= gems;
    }

    public void SubstractGold(int gold)
    {
        User.Instance.gold -= gold;
    }

    public bool hasEnoughGold(int gold)
    {
        return User.Instance.gold >= gold;
    }

    public bool hasEnoughGems(int gems)
    {
        return User.Instance.gems >= gems;
    }
}
