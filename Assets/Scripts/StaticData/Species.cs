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
        {SpeciesNames.Orc, new Dictionary<string, float>{{"hp", 16},{"damage", 4},{"speed", 5}}},
        {SpeciesNames.Golem, new Dictionary<string, float>{{"hp", 32},{"damage", 2},{"speed", 5}}},
        {SpeciesNames.FallenAngel, new Dictionary<string, float>{{"hp", 24},{"damage", 3},{"speed", 5}}},
    };

    public static readonly Dictionary<SpeciesNames, Dictionary<string, float>> statsPerLevel =
    new Dictionary<SpeciesNames, Dictionary<string, float>>
    {
        {SpeciesNames.Orc, new Dictionary<string, float>{{"hp", 6},{"damage", 3},{"speed", 1}}},
        {SpeciesNames.Golem, new Dictionary<string, float>{{"hp", 12},{"damage", 1},{"speed", 1}}},
        {SpeciesNames.FallenAngel, new Dictionary<string, float>{{"hp", 9},{"damage", 2},{"speed", 1}}},
    };
}