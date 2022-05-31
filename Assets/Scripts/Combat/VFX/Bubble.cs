using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator hitVfxAnimator = fighter.transform.Find("VFX/Bubble_VFX").GetComponent<Animator>();
        hitVfxAnimator.Play("bubble_0", -1, 0f);
    }
}