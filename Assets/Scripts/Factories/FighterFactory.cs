using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FighterFactory
{
    //FIXME: In the future dont give a default for species.
    public static Fighter CreatePlayerFighterInstance(string fighterName, float hp = 10, float damage = 1, float speed = 3, string species = "fire", 
        string skin = "RobotV1", int level = 1, int experiencePoints = 0, int manaSlots = 10, List<Card> cards = null)
    {
        Fighter fighter = PlayerUtils.FindInactiveFighter();
        fighter.FighterConstructor(fighterName, hp, damage, speed, species, skin, level, experiencePoints, manaSlots, cards);
        fighter.EnableSave();
        return fighter;
    }
}
