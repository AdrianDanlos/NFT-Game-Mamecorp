using UnityEngine;

public class Lightning : MonoBehaviour
{
    public static void StartAnimation(Fighter fighter)
    {
        Animator hitVfxAnimator = fighter.transform.Find("VFX/Lightning_VFX").GetComponent<Animator>();
        hitVfxAnimator.Play("lightning_0", -1, 0f);
    }
}