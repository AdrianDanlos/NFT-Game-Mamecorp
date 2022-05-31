using System.Collections.Generic;
using UnityEngine;

//TODO: Try to reuse this class for all of the combat VFX
public class VFXManager
{
    private static AnimationClip[] LoadBloodVFX()
    {
        return Resources.LoadAll<AnimationClip>("Animations/VFX/Blood");
    }

    public static void SetAnimationClipToAnimator(Fighter fighter)
    {
        AnimationClip[] bloodClips = LoadBloodVFX();
        int randomIndex = Random.Range(0, bloodClips.Length);
        var bloodAnimatorController = fighter.transform.Find("Hit_VFX").GetComponent<Animator>().runtimeAnimatorController;
        
        AnimatorOverrideController aoc = new AnimatorOverrideController(bloodAnimatorController);

        AnimationClip currentClip = bloodAnimatorController.animationClips[0];
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(currentClip, bloodClips[randomIndex]));
        aoc.ApplyOverrides(anims);
        bloodAnimatorController = aoc;

        Debug.Log(randomIndex);
        Debug.Log(anims[0]);
        Debug.Log(aoc.animationClips[0]);        
        Debug.Log(aoc.animationClips.Length);        
    }

    private static AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            Debug.Log(clip.name);
            if (clip.name == name)
            {
                Debug.Log(clip.name);
                return clip;
            }
        }
        return null;
    }
}