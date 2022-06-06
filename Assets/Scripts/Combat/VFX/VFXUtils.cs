using UnityEngine;

public class VFXUtils
{
    public static Color GetUsedButtonColor(Color color)
    {
        color.a = .4f;
        return color;
    }
}