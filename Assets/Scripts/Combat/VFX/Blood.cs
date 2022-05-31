using UnityEngine;

public class Blood : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator hitVfxAnimator = fighter.transform.Find("VFX/Hit_VFX").GetComponent<Animator>();
        hitVfxAnimator.Play(VFXManager.GetRandomBloodClipName(), -1, 0f);
    }
}