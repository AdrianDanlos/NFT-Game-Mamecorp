using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FighterFactory
{
    public static Fighter CreateFighterInstance(string fighterName, float hp = 10, float damage = 1, float speed = 3, string species = "fire", 
        string skin = "RobotV1", int level = 1, int experiencePoints = 0, int manaSlots = 10, List<Card> cards = null, bool isBot = false)
    {
        Fighter fighter = PlayerUtils.FindInactiveFighter();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, skin, level, experiencePoints, manaSlots, cards, isBot);
        return fighter;
    }
}
