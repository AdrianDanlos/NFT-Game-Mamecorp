using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FighterFactory
{
    public static Fighter CreateFighterInstance(string fighterName, float hp = 10, float damage = 1, float speed = 3, string species = "fire", int level = 1, int experiencePoints = 0, int manaSlots = 10, List<Card> cards = null)
    {
        Fighter fighter = PlayerUtils.FindInactiveFighter();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, level, experiencePoints, manaSlots, cards);
        return fighter;
    }
}
