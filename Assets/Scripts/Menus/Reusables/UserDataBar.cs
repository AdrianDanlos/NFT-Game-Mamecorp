using UnityEngine;
using System.Collections;

public class UserDataBar : MonoBehaviour
{
    public GameObject gold;
    public GameObject gems;
    public GameObject energy;
    public GameObject timerContainer;
    public GameObject timer;
    public GameObject playerNameGO;

    IEnumerator Start()
    {
        Fighter player = PlayerUtils.FindInactiveFighter();
        MenuUtils.SetGold(gold);
        MenuUtils.SetGems(gems);
        EnergyManager.RefreshEnergyBasedOnCountdown();
        MenuUtils.SetEnergy(energy);
        MenuUtils.SetName(playerNameGO, player.fighterName);

        //Update timer each second
        while (!EnergyManager.UserHasMaxEnergy())
        {
            MenuUtils.DisplayEnergyCountdown(timerContainer, timer);
            yield return new WaitForSeconds(1f);
        }

        timerContainer.SetActive(false);
    }

    void OutputTime()
    {
        Debug.Log(Time.time);
    }

}