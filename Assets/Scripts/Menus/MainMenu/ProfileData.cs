using UnityEngine;

public class ProfileData
{
    public static void SaveFights()
    {
        PlayerPrefs.SetFloat("fights", PlayerPrefs.GetFloat("fights") + 1);
        PlayerPrefs.Save();
    }

    public static void SavePeakElo(int elo)
    {
        if (elo > User.Instance.peakElo) User.Instance.peakElo = elo;
    }

    public static void SaveCups()
    {
        User.Instance.cups++;
    }
}
