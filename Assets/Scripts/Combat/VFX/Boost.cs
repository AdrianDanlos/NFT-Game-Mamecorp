using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Boost : MonoBehaviour
{
    float boostDuration = 5f;
    bool isPlayerBoostActive;
    bool isBotBoostActive;

    //Entrypoint for user
    public void OnClickTriggerBoostEffects()
    {        
        Image iconImage = this.transform.Find("Icon").GetComponent<Image>();
        iconImage.color = VFXUtils.GetUsedButtonColor(iconImage.color);
        this.GetComponent<Button>().interactable = false;
        TriggerBoostEffects(Combat.player);
    }

    private void StartParticlesAnimation(Fighter fighter)
    {
        fighter.transform.Find("VFX/Boost_VFX/Particles_VFX").GetComponent<ParticleSystem>().Play();
    }

    //Entrypoint for bot
    public void TriggerBoostEffects(Fighter fighter)
    {
        SetIsBoostActiveValue(fighter, true);

        Animator lightningAnimator = fighter.transform.Find("VFX/Boost_VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
        Debug.Log(fighter.damage);
        fighter.damage *= 2f;
        Debug.Log(fighter.damage);
        fighter.GetComponent<Renderer>().material.color = new Color32(255, 192, 0, 255);
        StartCoroutine(StartBoostTimer(fighter));
        StartCoroutine(ShowParticlesWhileBoostLast(fighter));
    }

    private void SetIsBoostActiveValue(Fighter fighter, bool isBoostActive)
    {
        if (Combat.player == fighter) isPlayerBoostActive = isBoostActive;
        else isBotBoostActive = isBoostActive;
    }


    IEnumerator StartBoostTimer(Fighter fighter)
    {
        yield return new WaitForSeconds(boostDuration);
        SetIsBoostActiveValue(fighter, false);
    }

    IEnumerator ShowParticlesWhileBoostLast(Fighter fighter)
    {
        while (isBoostActive(fighter))
        {
            StartParticlesAnimation(fighter);
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(RemoveBoostEffects(fighter));
    }

    IEnumerator RemoveBoostEffects(Fighter fighter)
    {
        Debug.Log("Reset");
        Debug.Log(fighter.damage);
        fighter.damage /= 2f;
        Debug.Log(fighter.damage);
        fighter.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        //Wait for particles animation to finish
        yield return new WaitForSeconds(1f);
    }

    private bool isBoostActive(Fighter fighter)
    {
        return Combat.player == fighter ? isPlayerBoostActive : isBotBoostActive;
    }
}