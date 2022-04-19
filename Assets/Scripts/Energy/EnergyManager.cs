using UnityEngine;
using System;
public static class EnergyManager
{
    public static void SubtractOneEnergyPoint()
    {
        if (User.Instance.energy == PlayerUtils.maxEnergy) StartCountdown();
        if (User.Instance.energy == 0) Debug.Log("ERROR, THE USER ENERGY IS ALREADY 0!"); //FIXME: Fix this bug
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
        DateTime countdownEndTime = GetCountdownEndTime();
        return DateTime.Now.CompareTo(countdownEndTime) > 0;
    }

    public static bool UserHasMaxEnergy()
    {
        return User.Instance.energy == PlayerUtils.maxEnergy;
    }

    public static DateTime GetCountdownEndTime()
    {
        long tempEndTime = Convert.ToInt64(PlayerPrefs.GetString("countdownEndTime"));
        return DateTime.FromBinary(tempEndTime);
    }
}