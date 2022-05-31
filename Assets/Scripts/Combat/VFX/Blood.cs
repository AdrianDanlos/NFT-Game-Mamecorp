using UnityEngine;

public class Blood : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator bloodAnimator = fighter.transform.Find("VFX/Hit_VFX").GetComponent<Animator>();
        bloodAnimator.Play(VFXManager.GetRandomBloodClipName(), -1, 0f);
    }
}