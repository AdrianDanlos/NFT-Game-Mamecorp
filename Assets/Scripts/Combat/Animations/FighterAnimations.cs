using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FighterAnimations
{
    public enum AnimationNames
    {
        IDLE,
        RUN,
        ATTACK,
        JUMP,
        DEATH,
    }

    public static void ChangeAnimation(Fighter fighter, AnimationNames newAnimation){
        fighter.GetComponent<Animator>().Play(newAnimation.ToString());
    }
}