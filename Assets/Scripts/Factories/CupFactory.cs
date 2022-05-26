using System.Collections.Generic;

public static class CupFactory
{
    public static void CreateCupInstance(string cupName, bool isActive, string round, List<CupFighter> participants, Dictionary<string, Dictionary<string, Dictionary<string, string>>> cupInfo)
    {
        Cup cup = Cup.Instance;
        cup.SetCupValues(cupName, isActive, round, participants, cupInfo);
        cup.EnableSave();
    }
}
