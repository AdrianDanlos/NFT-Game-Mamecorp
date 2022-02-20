using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FighterFactory
{
    public static Fighter CreatePlayerFighterInstance(string fighterName, string skin, string species, float hp, float damage, float speed,
        int level = 1, int experiencePoints = 0, int manaSlots = 10, List<Card> cards = null)
    {
        Fighter fighter = PlayerUtils.FindInactiveFighter();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, skin, level, experiencePoints, manaSlots, cards);
        fighter.EnableSave();
        return fighter;
    }
}
