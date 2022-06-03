using UnityEngine;
using UnityEngine.UI;

public class Lightning : MonoBehaviour
{
    public void OnClickStartLightningAnimation()
    {
        this.GetComponent<Button>().interactable = false;
        Animator lightningAnimator = Combat.player.transform.Find("VFX/Boost_VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
        StartParticlesAnimation();
    }

    private void StartParticlesAnimation()
    {
        Combat.player.transform.Find("VFX/Boost_VFX/Particles_VFX").GetComponent<ParticleSystem>().Play();
    }
}