using UnityEngine;
using System;
public static class EnergyManager
{
    public static int defaultTimeOfRefreshInHours = 4;
    public static void SubtractOneEnergyPoint()
    {
        if (User.Instance.energy == PlayerUtils.maxEnergy) StartCountdown();
        if (User.Instance.energy == 0) Debug.Log("ERROR, THE USER ENERGY IS ALREADY 0!"); //FIXME: Fix this bug
        User.Instance.energy--;
    }

    public static void StartCountdown()
    {
        DateTime countdownEndTime = DateTime.Now.AddHours(defaultTimeOfRefreshInHours);

        //Save the countdownEndTime to player prefs so they can be recovered after closing the game
        PlayerPrefs.SetString("countdownEndTime", countdownEndTime.ToBinary().ToString());
    }

    public static bool UserHasMaxEnergy()
    {
        return User.Instance.energy == PlayerUtils.maxEnergy;
    }

    public static TimeSpan GetTimeUntilCountDownEnds()
    {
        long tempEndTime = Convert.ToInt64(PlayerPrefs.GetString("countdownEndTime"));
        return DateTime.FromBinary(tempEndTime) - DateTime.Now;
    }

    public static void RefreshEnergyBasedOnCountdown()
    {
        Debug.Log("Hours");
        Debug.Log(GetTimeUntilCountDownEnds().Hours);
        Debug.Log("Minutes");
        Debug.Log(GetTimeUntilCountDownEnds().Minutes);
        Debug.Log("Seconds");
        Debug.Log(GetTimeUntilCountDownEnds().Seconds);

        if (!UserHasMaxEnergy())
        {
            //Rrefresh 0-n energy
            User.Instance.energy += (int)Mathf.Floor(GetTimeUntilCountDownEnds().Hours / defaultTimeOfRefreshInHours);
        }
    }
}