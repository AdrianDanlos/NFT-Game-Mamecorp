using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChooseFirstFighterAnimations
{
    public static void SetFightersSkin(Animator animator)
    {
        //AnimationClip[] idleAnimation = Resources.LoadAll<AnimationClip>("Animations/Characters/" + "MonsterV5/" + "01_idle");
        //AnimationClip idleAnimation = Resources.Load<AnimationClip>("Animations/Characters/MonsterV5/01_idle.anim");
        AnimationClip idleAnimation = Resources.LoadAll<AnimationClip>("Animations/Characters/" + "MonsterV5/")[0];
        SetAnimationClipToAnimator(animator, idleAnimation);
    }

    private static void SetAnimationClipToAnimator(Animator animator, AnimationClip idleAnimation)
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        int index = 0;

        foreach (var defaultClip in aoc.animationClips)
        {
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(defaultClip, idleAnimation));
            index++;
        }

        aoc.ApplyOverrides(anims);
        animator.runtimeAnimatorController = aoc;
    }
}
