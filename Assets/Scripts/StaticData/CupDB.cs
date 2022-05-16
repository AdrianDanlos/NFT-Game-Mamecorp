using System.Collections.Generic;

public static class CupDB 
{
    public enum CupNames
    {
        EARTH,
        WATER,
        AIR,
        FIRE
    }

    public enum CupRounds
    {
        QUARTERS,
        SEMIS,
        FINALS,
        END
    }

    // rewards given if each round if passed
    public static readonly Dictionary<CupRounds, Dictionary<string, string>> prizes =
        new Dictionary<CupRounds, Dictionary<string, string>>
        {
            {
                CupRounds.QUARTERS, new Dictionary<string, string>
                {
                    {"reward", "gold"},
                    {"value", "1000" }
                }
            },
            {
                CupRounds.SEMIS, new Dictionary<string, string>
                {
                    {"reward", "gems"},
                    {"value", "50"}
                }
            },
            {
                CupRounds.FINALS, new Dictionary<string, string>
                {
                    {"reward", "chest"},
                    {"value", "EPIC"}
                }
            },
        };
}
