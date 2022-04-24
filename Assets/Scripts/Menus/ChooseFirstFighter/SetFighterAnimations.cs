using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetFighterAnimations : MonoBehaviour
{
    public Animator fighterAnimator;
    private void Start()
    {
        string skinName;
        fighterAnimator = this.GetComponent<Animator>();

        if (IsChooseFirstFighterMenu()) skinName = this.GetComponent<FighterSkinData>().skinName;
        //For Main menu, loading screen...
        else skinName = PlayerUtils.FindInactiveFighter().skin;

        //TODO: This script only sets the idle animation. Change it to be more flexible
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

    private bool IsChooseFirstFighterMenu()
    {
        return SceneManager.GetActiveScene().name == SceneNames.ChooseFirstFighter.ToString();
    }
}
