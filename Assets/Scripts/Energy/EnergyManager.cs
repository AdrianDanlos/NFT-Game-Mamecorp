using UnityEngine;
using System;
public static class EnergyManager
{
    public static int defaultEnergyRefreshTimeInMinutes = 5;
    public static void SubtractOneEnergyPoint()
    {
        if (User.Instance.energy == PlayerUtils.maxEnergy) StartCountdown();
        if (User.Instance.energy == 0) Debug.Log("ERROR, THE USER ENERGY IS ALREADY 0!"); //FIXME: Fix this bug
        User.Instance.energy--;
    }

    public static void StartCountdown()
    {
        //Save the countdownStartTime to player prefs so they can be recovered after closing the game
        PlayerPrefs.SetString("countdownStartTime", DateTime.Now.ToBinary().ToString());
    }

    public static bool UserHasMaxEnergy()
    {
        return User.Instance.energy == PlayerUtils.maxEnergy;
    }

    public static long GetCountDownStartTime()
    {
        bool countdownPlayerPrefExists = PlayerPrefs.GetString("countdownStartTime") != "";
        return countdownPlayerPrefExists ? Convert.ToInt64(PlayerPrefs.GetString("countdownStartTime")) : 0;
    }

    public static TimeSpan GetTimeSinceCountdownStart()
    {
        return DateTime.Now - DateTime.FromBinary(GetCountDownStartTime());
    }

    public static void RefreshEnergyBasedOnCountdown()
    {
        //DebugCountdownHelper();

        if (!UserHasMaxEnergy())
        {
            //Refresh 0-n energy. 
            int energyToAdd = (int)Mathf.Floor(GetTimeSinceCountdownStart().Minutes / defaultEnergyRefreshTimeInMinutes);
            int updatedEnergy = User.Instance.energy + energyToAdd;
            User.Instance.energy = updatedEnergy > PlayerUtils.maxEnergy ? PlayerUtils.maxEnergy : updatedEnergy;

            UpdateCountdown();
        }
    }

    public static void UpdateCountdown()
    {
        double minutesPassed = GetTimeSinceCountdownStart().TotalMinutes;
        double minutesLeftOnCurrentCountdown = minutesPassed % defaultEnergyRefreshTimeInMinutes;
        double minutesToAddToPreviousCountdown = minutesPassed - minutesLeftOnCurrentCountdown;

        DateTime newCountdown = DateTime.FromBinary(GetCountDownStartTime()).AddMinutes(minutesToAddToPreviousCountdown);
        PlayerPrefs.SetString("countdownStartTime", newCountdown.ToBinary().ToString());
    }

    private static void DebugCountdownHelper(){
        Debug.Log("GetTimeSinceCountdownStart");
        Debug.Log(GetTimeSinceCountdownStart());
        Debug.Log("Hours");
        Debug.Log(GetTimeSinceCountdownStart().Hours);
        Debug.Log("Minutes");
        Debug.Log(GetTimeSinceCountdownStart().Minutes);
        Debug.Log("Seconds");
        Debug.Log(GetTimeSinceCountdownStart().Seconds);
        Debug.Log("Energies refreshed");
        Debug.Log((int)Mathf.Floor(GetTimeSinceCountdownStart().Minutes / defaultEnergyRefreshTimeInMinutes));
    }
}