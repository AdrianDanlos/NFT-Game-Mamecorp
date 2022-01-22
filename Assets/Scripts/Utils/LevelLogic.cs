using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        levelValue.text = Levels.CalculateLevel(xp).ToString();
        XPValue.text = xp.ToString();
    }
}
