using System;

public class GeneralUtils
{
    //To disable simulation set this to false
    public const bool SimulationEnabled = true;
    public const float SimulationTime = 0f;
    public static double ToSingleDecimal(double number)
    {
        string numberAsString = number.ToString();
        int startingPositionToTrim = 3;

        string trimmedString = numberAsString.Remove(startingPositionToTrim, numberAsString.Length - startingPositionToTrim);
        return Convert.ToDouble(trimmedString);
    }

    public static SpeciesNames StringToSpeciesNamesEnum(string species)
    {
        return (SpeciesNames)Enum.Parse(typeof(SpeciesNames), species);
    }

    public static SkillCollection.SkillRarity StringToSkillRarityEnum(string rarity)
    {
        return (SkillCollection.SkillRarity)Enum.Parse(typeof(SkillCollection.SkillRarity), rarity);
    }

    public static float GetRealOrSimulationTime(float realTimeWait)
    {
        return SimulationEnabled ? SimulationTime : realTimeWait;
    }

    public static string GetRandomSpecies()
    {
        Array species = Enum.GetValues(typeof(SpeciesNames));
        int a = species.Length;
        return species.GetValue(UnityEngine.Random.Range(0, species.Length)).ToString();
    }
}