using UnityEngine;
//Utility class for VFX 
public class VFXManager
{
    public static string GetRandomBloodClipName()
    {
        var bloodAnimatorController = GameObject.Find("Hit_VFX").GetComponent<Animator>().runtimeAnimatorController;
        int randomIndex = Random.Range(0, bloodAnimatorController.animationClips.Length);
        return bloodAnimatorController.animationClips[randomIndex].name;
    }
}