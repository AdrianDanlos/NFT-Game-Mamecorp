using UnityEngine;

public class CollectiblesBar : MonoBehaviour
{
    public GameObject gold;
    public GameObject gems;
    public GameObject energy;
    public GameObject timerContainer;
    public GameObject timer;

    void Start()
    {
        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        EnergyManager.RefreshEnergyBasedOnCountdown();
        MenuUtils.SetEnergy(energy);
        MenuUtils.DisplayEnergyCountdown(timerContainer, timer);
    }
}