using UnityEngine;
using System;
public static class EnergyManager
{

    public static DateTime countdownStartTime;
    public static DateTime countdownEndTime;
    public static void SubtractOneEnergyPoint()
    {
        if (User.Instance.energy == PlayerUtils.maxEnergy) StartCountdown();
        User.Instance.energy--;
    }

    public static void StartCountdown()
    {
        countdownStartTime = DateTime.Now;
        countdownEndTime = countdownStartTime.AddHours(4);
    }

    public static bool IsCountdownOver()
    {
        return countdownStartTime.CompareTo(countdownEndTime) > 0;
    }
}