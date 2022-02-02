using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//FIXME: Do we need this class?
public class LevelLogic : MonoBehaviour
{
    Text levelValue;
    Text XPValue;
    public static int xp;

    void Start()
    {
        levelValue = this.transform.Find("LevelValue").GetComponent<Text>();
        XPValue = this.transform.Find("XPValue").GetComponent<Text>();
    }

    private void Update()
    {
        levelValue.text = CalculateLevel(xp).ToString();
        XPValue.text = xp.ToString();
    }

    public static int CalculateLevel(int totalXP)
    {
        return totalXP / 2;
    }
}
