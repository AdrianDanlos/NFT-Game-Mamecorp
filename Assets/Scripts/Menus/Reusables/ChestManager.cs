using System.Collections.Generic;
using UnityEngine;

public static class ChestManager
{
    const float TOTALWEIGHT = 100f;

    public static string GetRandomShopChestRarity(string chestType)
    {
        chestType = chestType.ToUpper();

        float diceRoll = Random.Range(0f, TOTALWEIGHT);

        foreach (KeyValuePair<string, float> chest in Chest.shopChests[(Chest.ShopChestTypes)System.Enum.Parse(typeof(Chest.ShopChestTypes), chestType.ToUpper())])
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        Debug.Log("error opening chest");
        return null;
    }

    public static Chest.BattleChestRarities GetRandomBattleChestRarity()
    {
        float diceRoll = Random.Range(0f, TOTALWEIGHT);

        foreach (KeyValuePair<Chest.BattleChestRarities, float> chest in
            Chest.battleChestsProbabilities)
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        Debug.Log("ERROR. Something went wrong when getting a random chest");
        return Chest.BattleChestRarities.COMMON;
    }
}
