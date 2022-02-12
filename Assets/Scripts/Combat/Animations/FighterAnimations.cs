using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FighterAnimations : MonoBehaviour
{
    public enum AnimationNames
    {
        IDLE,
        RUN,
        ATTACK,
        JUMP,
        DEATH,
    }

    public static readonly Dictionary<FighterAnimations.AnimationNames, float> animationDuration = new Dictionary<FighterAnimations.AnimationNames, float>
    {
        {AnimationNames.IDLE, 0},
        {AnimationNames.RUN, 0},
        {AnimationNames.ATTACK, 0.4f},
        {AnimationNames.JUMP, 1f},
        {AnimationNames.DEATH, 1f},
    };

    public static IEnumerator ChangeAnimation(Fighter fighter, AnimationNames newAnimation)
    {
        fighter.GetComponent<Animator>().Play(newAnimation.ToString());
        fighter.currentAnimation = newAnimation.ToString();
        yield return new WaitForSeconds(animationDuration[newAnimation]);
    }
}