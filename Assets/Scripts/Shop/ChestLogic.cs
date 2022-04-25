using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLogic : MonoBehaviour
{
    public string OpenChest(string chestType)
    {
        // TODO: make code reusable in post combat chest

        float totalWeight = 100f;
        float diceRoll = Random.Range(0f, totalWeight);
        List<float> weights = new List<float>();

        foreach (KeyValuePair<string, float> chest in 
            Chest.shopChests[(ShopChestTypes)System.Enum.Parse(typeof(ShopChestTypes), chestType.ToUpper())])
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        return null;
    }
}
