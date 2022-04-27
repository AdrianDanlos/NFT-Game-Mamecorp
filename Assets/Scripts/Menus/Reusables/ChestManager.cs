using System.Collections.Generic;
using UnityEngine;

public static class ChestManager
{
    const float TOTALWEIGHT = 100f;

    public static string OpenChest(string chestType)
    {
        chestType = chestType.ToUpper();
        // TODO: make code reusable in post combat chest

        float diceRoll = Random.Range(0f, TOTALWEIGHT);

        foreach (KeyValuePair<string, float> chest in
            Chest.shopChests[(Chest.ShopChestTypes)System.Enum.Parse(typeof(Chest.ShopChestTypes), chestType.ToUpper())])
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        Debug.Log("error opening chest");
        return null;
    }

    public static Chest.BattleChestRarities GetRandomBattleChest()
    {
        float diceRoll = Random.Range(0f, TOTALWEIGHT);

        foreach (KeyValuePair<Chest.BattleChestRarities, float> chest in
            Chest.battleChestsProbabilities)
        {
            if (chest.Value >= diceRoll)
                return chest.Key;

            diceRoll -= chest.Value;
        }

        Debug.Log("chest error");
        return Chest.BattleChestRarities.COMMON;
    }

    public static int GetBattleChestGold(string battleChest)
    {
        battleChest = battleChest.ToUpper();
        return Random.Range(
            Chest.battleChestsRewards[(Chest.BattleChestRarities)System.Enum.Parse(typeof(Chest.BattleChestRarities), battleChest)]["minGold"],
            Chest.battleChestsRewards[(Chest.BattleChestRarities)System.Enum.Parse(typeof(Chest.BattleChestRarities), battleChest)]["maxGold"]);
    }

    public static int GetBattleChestGems(string battleChest)
    {
        battleChest = battleChest.ToUpper();
        return Random.Range(
                    Chest.battleChestsRewards[(Chest.BattleChestRarities)System.Enum.Parse(typeof(Chest.BattleChestRarities), battleChest)]["minGems"],
                    Chest.battleChestsRewards[(Chest.BattleChestRarities)System.Enum.Parse(typeof(Chest.BattleChestRarities), battleChest)]["maxGems"]);
    }

    public static SkillCollection.SkillRarity GetBattleChestSkillReward(string battleChest)
    {
        battleChest.ToUpper();
        float diceRoll = Random.Range(0f, TOTALWEIGHT);

        foreach (KeyValuePair<string, float> raity in
        Chest.battleChestsSkillProbabilities
        [(Chest.BattleChestRarities)System.Enum.Parse(typeof(Chest.BattleChestRarities), battleChest)])
        {
            if (raity.Value >= diceRoll)
                return (SkillCollection.SkillRarity)System.Enum.Parse(typeof(SkillCollection.SkillRarity), raity.Key);

            diceRoll -= raity.Value;
        }

        Debug.Log("chest error");
        return SkillCollection.SkillRarity.COMMON;
    }
}
