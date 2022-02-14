using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFighterAnimations : MonoBehaviour
{
    public Animator fighterAnimator;
    private void Start()
    {
        fighterAnimator = this.GetComponent<Animator>();
        string skinName = this.GetComponent<FighterSkinName>().skinName;
        AnimationClip idleAnimation = Resources.Load<AnimationClip>("Animations/Characters/" + skinName + "/01_idle");
        SetAnimationClipToAnimator(fighterAnimator, idleAnimation);
    }

    private static void SetAnimationClipToAnimator(Animator animator, AnimationClip idleAnimation)
    {
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        AnimationClip defaultIdleClip = aoc.animationClips[0];

        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(defaultIdleClip, idleAnimation));
        aoc.ApplyOverrides(anims);
        animator.runtimeAnimatorController = aoc;
    }
}
