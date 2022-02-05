using System.Collections.Generic;
using UnityEngine;

public static class FighterSkin
{
    public static void SetFightersSkin(Fighter player, Fighter bot)
    {
        SetSkin(player);
        SetSkin(bot);
    }

    private static void SetSkin(Fighter fighter){
        LoadFighterSkin(fighter);
        SetAnimationClipToAnimator(fighter);
    }

    private static void LoadFighterSkin(Fighter fighter)
    {
        fighter.skinAnimations = Resources.LoadAll<AnimationClip>("Animations/" + fighter.skin);
    }

    private static void SetAnimationClipToAnimator(Fighter fighter)
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(fighter.animator.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        int index = 0;

        foreach (var defaultClip in aoc.animationClips)
        {
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(defaultClip, fighter.skinAnimations[index]));
            index++;
        }

        aoc.ApplyOverrides(anims);
        fighter.animator.runtimeAnimatorController = aoc;
    }
}