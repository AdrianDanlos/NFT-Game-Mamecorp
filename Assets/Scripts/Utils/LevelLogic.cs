using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLogic : MonoBehaviour
{
    Text levelValue;
    public static int xp;

    void Start()
    {
        levelValue = this.transform.Find("LevelValue").GetComponent<Text>();
    }

    private void Update()
    {
        levelValue.text = Levels.CalculateLevel(xp).ToString();
    }
}
