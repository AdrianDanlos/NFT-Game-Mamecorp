using UnityEngine;
using System;
public static class EnergyManager
{
    public static void SubtractOneEnergyPoint()
    {
        if (User.Instance.energy == PlayerUtils.maxEnergy) StartCountdown();
        User.Instance.energy--;
    }

    public static void StartCountdown()
    {
        DateTime countdownEndTime = DateTime.Now.AddHours(4);

        //Save the countdownEndTime to player prefs so they can be recovered after closing the game
        PlayerPrefs.SetString("countdownEndTime", countdownEndTime.ToBinary().ToString());
    }

    public static bool IsCountdownOver()
    {
        long tempEndTime = Convert.ToInt64(PlayerPrefs.GetString("countdownEndTime"));
        DateTime countdownEndTime = DateTime.FromBinary(tempEndTime);
        return DateTime.Now.CompareTo(countdownEndTime) > 0;
    }

    public static bool UserHasMaxEnergy()
    {
        return User.Instance.energy == PlayerUtils.maxEnergy;
    }
}