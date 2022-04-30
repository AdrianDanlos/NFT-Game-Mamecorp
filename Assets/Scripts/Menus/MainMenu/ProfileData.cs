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
        // TODO
        PlayerPrefs.SetFloat("cups", 0);
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
