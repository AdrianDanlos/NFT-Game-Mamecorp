using UnityEngine;

public class ProfileData 
{
    public static void SaveFights()
    {
        PlayerPrefs.SetFloat("fights", PlayerPrefs.GetFloat("fights") + 1);
        PlayerPrefs.Save();
    }

    public static void SaveHighestTrophies(float trophies)
    {
        if (trophies > PlayerPrefs.GetFloat("maxTrophies"))
        {
            PlayerPrefs.SetFloat("maxTrophies", trophies);
            PlayerPrefs.Save();
        }
    }

    public static void SaveCups()
    {
        PlayerPrefs.SetFloat("cups", PlayerPrefs.GetFloat("cups") + 1);
        PlayerPrefs.Save();
    }

    public static void SaveHighestEnemy(float enemyCups)
    {
        if(enemyCups > PlayerPrefs.GetFloat("highestEnemy"))
        {
            PlayerPrefs.SetFloat("highestEnemy", enemyCups);
            PlayerPrefs.Save();
        }
    }
}
