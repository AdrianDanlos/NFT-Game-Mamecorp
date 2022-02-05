using System.Collections.Generic;
using UnityEngine;

public static class FighterSkin
{
    public static void SetFighterSkin(Fighter fighter, string fighterSkin)
    {
        Debug.Log(fighterSkin);
        //Load skin
        fighter.skinAnimations = Resources.LoadAll<AnimationClip>("Animations/" + fighterSkin);
        Debug.Log(fighter.skinAnimations);
        Debug.Log(fighter.animator.runtimeAnimatorController);

        //Set loaded animationclip to anmiator
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