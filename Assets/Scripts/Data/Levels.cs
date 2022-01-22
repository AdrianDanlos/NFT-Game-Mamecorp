using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Levels 
{
    public static int CalculateLevel(int totalXP)
    {
        return totalXP / 2;
    }
}
