using UnityEngine;

public class CollectiblesBar : MonoBehaviour {
    public GameObject gold;
    public GameObject gems;
    public GameObject energy;

    void Start()
    {
        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        SetEnergy();
    }
    private void SetEnergy()
    {
        if (User.Instance.energy < PlayerUtils.maxEnergy
            && PlayerPrefs.HasKey("countdownEndTime")
            && EnergyManager.IsCountdownOver())
        {
            User.Instance.energy++;
            if (User.Instance.energy < PlayerUtils.maxEnergy) EnergyManager.StartCountdown();
        }

        //Set energy timer
    }
}