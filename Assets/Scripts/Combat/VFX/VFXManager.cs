using System.Collections.Generic;
using UnityEngine;

//TODO: Try to reuse this class for all of the combat VFX. Currently only for blood.
public class VFXManager
{
    public static string GetRandomClipName()
    {
        var bloodAnimatorController = GameObject.Find("Hit_VFX").GetComponent<Animator>().runtimeAnimatorController;
        int randomIndex = Random.Range(0, bloodAnimatorController.animationClips.Length);
        return bloodAnimatorController.animationClips[randomIndex].name;
    }
}