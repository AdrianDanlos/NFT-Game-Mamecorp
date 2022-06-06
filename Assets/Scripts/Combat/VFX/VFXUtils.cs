using UnityEngine;

public class VFXUtils : MonoBehaviour
{
    public static Color GetUsedButtonColor(Color color)
    {
        color.a = .4f;
        return color;
    }

    public static void DisplayFloatingHp(Fighter defender, GameObject floatingHp, float hpChange, Color? color = null)
    {
        Vector3 floatingHpPosition = defender.transform.position;
        //Displace text for bot
        if (defender == Combat.bot) floatingHpPosition.x += 2;
        GameObject floatingHpInstance = Instantiate(floatingHp, floatingHpPosition, Quaternion.identity, defender.transform);
        floatingHpInstance.GetComponent<FloatingHp>().StartAnimation(hpChange, color);
    }
}