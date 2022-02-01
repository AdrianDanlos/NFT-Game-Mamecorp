using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FighterFactory
{
    private static GameObject fighterGameObject;
    public static Fighter CreateFighterInstance(string fighterName, float hp = 10, float damage = 1, float speed = 3, string species = "fire", int level = 1, int manaSlots = 10, List<Card> cards = null)
    {
        fighterGameObject = PlayerUtils.FindInactiveFighterGameObject();
        Fighter fighter = fighterGameObject.GetComponent<Fighter>();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, level, manaSlots, cards);
        return fighter;
    }
}
