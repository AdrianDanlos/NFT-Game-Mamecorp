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

        if (IsMainMenu()) skinName = PlayerUtils.FindInactiveFighter().skin;
        //For ChooseFirstFighter Menu
        else skinName = this.GetComponent<FighterSkinName>().skinName;

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

    private bool IsMainMenu()
    {
        return SceneManager.GetActiveScene().name == SceneNames.MainMenu.ToString();
    }
}
