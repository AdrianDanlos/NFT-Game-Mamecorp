using UnityEngine;

public class CollectiblesBar : MonoBehaviour
{
    public GameObject gold;
    public GameObject gems;
    public GameObject energy;

    void Start()
    {
        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        UpdateUserEnergyBasedOnCountdown();
        MenuUtils.SetEnergy(energy);
        MenuUtils.DisplayEnergyCountdown();
    }

    private void UpdateUserEnergyBasedOnCountdown()
    {
        if (!EnergyManager.UserHasMaxEnergy()
            && EnergyManager.IsCountdownOver())
        {
            User.Instance.energy++;
            if (!EnergyManager.UserHasMaxEnergy()) EnergyManager.StartCountdown();
        }
    }
}