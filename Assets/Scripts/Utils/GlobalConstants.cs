using UnityEngine;

//Game config variables
public class GlobalConstants
{
    //Simulation mode
    public static bool SimulationEnabled = false;
    public static float SimulationTime = 0f;

    //Skills
    public static int ProbabilityOfUsingSkillEachTurn = 50;

    //Colors
    public static Color noColor = new Color(1, 1, 1);
    public static Color healColor = new Color32(134, 255, 117, 255);
    public static Color criticalAttackColor = new Color32(240, 164, 0, 255);
}