using UnityEngine;

public class Blood : MonoBehaviour
{
    public enum AnimationNames
    {
        HIT
    }

    public static void StartAnimation(Fighter fighter)
    {
        Animator hitVfxAnimator = fighter.transform.Find("Hit_VFX").GetComponent<Animator>();
        hitVfxAnimator.Play(VFXManager.GetRandomClipName(), -1, 0f);
    }
}