using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLogic : MonoBehaviour
{
    float totalWeight = 100f;


    public void OpenChest(string chestType)
    {
        float diceRoll = Random.Range(0f, totalWeight);
        List<float> weights = new List<float>();

        
    }
}
