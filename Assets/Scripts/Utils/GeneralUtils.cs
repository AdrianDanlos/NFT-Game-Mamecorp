using System;
public class GeneralUtils
{
    public static double ToSingleDecimal(double number)
    {
        string numberAsString = number.ToString();
        int startingPositionToTrim = 3;

        string trimmedString = numberAsString.Remove(startingPositionToTrim, numberAsString.Length - startingPositionToTrim);
        return Convert.ToDouble(trimmedString);
    }

    public static SpeciesNames StringToSpeciesNamesEnum(string species){
        return (SpeciesNames)Enum.Parse(typeof(SpeciesNames), species);
    }
}