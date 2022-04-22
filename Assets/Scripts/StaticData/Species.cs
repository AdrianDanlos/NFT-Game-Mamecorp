using System.Collections.Generic;
public enum SpeciesNames
{
    FallenAngel,
    Golem,
    Orc
}

public class Species
{
    public static readonly Dictionary<SpeciesNames, Dictionary<string, float>> defaultStats =
    new Dictionary<SpeciesNames, Dictionary<string, float>>
    {
        {SpeciesNames.Orc, new Dictionary<string, float>{{"hp", 12},{"damage", 2.5f},{"speed", 5}}},
        {SpeciesNames.Golem, new Dictionary<string, float>{{"hp", 48},{"damage", 1.5f},{"speed", 5}}},
        {SpeciesNames.FallenAngel, new Dictionary<string, float>{{"hp", 24},{"damage", 2f},{"speed", 5}}},
    };

    public static readonly Dictionary<SpeciesNames, Dictionary<string, float>> statsPerLevel =
    new Dictionary<SpeciesNames, Dictionary<string, float>>
    {
        {SpeciesNames.Orc, new Dictionary<string, float>{{"hp", 6},{"damage", 2f},{"speed", 1}}},
        {SpeciesNames.Golem, new Dictionary<string, float>{{"hp", 24},{"damage", 1f},{"speed", 1}}},
        {SpeciesNames.FallenAngel, new Dictionary<string, float>{{"hp", 18},{"damage", 1.5f},{"speed", 1}}},
    };
}