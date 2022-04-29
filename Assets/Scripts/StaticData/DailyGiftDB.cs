using System.Collections.Generic;

public static class DailyGiftDB 
{
    public enum days
    {
        DAY1,
        DAY2,
        DAY3,
        DAY4,
        DAY5,
        DAY6,
        DAY7,
    }

    public static readonly Dictionary<days, Dictionary<string, string>> gifts =
        new Dictionary<days, Dictionary<string, string>>
        {
            {
                days.DAY1, new Dictionary<string, string>
                {
                    {"reward", "gold"},
                    {"value", "250" }
                }
            },
            {
                days.DAY2, new Dictionary<string, string>
                {
                    {"reward", "gold"},
                    {"value", "500"}
                }
            },
            {
                days.DAY3, new Dictionary<string, string>
                {
                    {"reward", "gems"},
                    {"value", "10"}
                }
            },
            {
                days.DAY4, new Dictionary<string, string>
                {
                    {"reward", "gold"},
                    {"value", "750"}
                }
            },
            {
                days.DAY5, new Dictionary<string, string>
                {
                    {"reward", "gold"},
                    {"value", "1000"}
                }
            },
            {
                days.DAY6, new Dictionary<string, string>
                {
                    {"reward", "gems"},
                    {"value", "20"}
                }
            },
            {
                days.DAY7, new Dictionary<string, string>
                {
                    {"reward", "chest"},
                    {"value", "EPIC"}
                }
            },
        };
}
