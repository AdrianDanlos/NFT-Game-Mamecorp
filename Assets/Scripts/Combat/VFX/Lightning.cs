using UnityEngine;

public class Boost : MonoBehaviour
{
    public static void StartLightningAnimation(Fighter fighter)
    {
        Animator lightningAnimator = fighter.transform.Find("VFX/Boost_VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
    }

    public static void StartParticlesAnimation(Fighter fighter)
    {
        fighter.transform.Find("VFX/Explosion_VFX").GetComponent<ParticleSystem>().Play();
    }
}