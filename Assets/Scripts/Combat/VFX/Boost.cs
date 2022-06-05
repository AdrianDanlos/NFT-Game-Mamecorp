using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Boost : MonoBehaviour
{
    float boostDuration = 5f;
    bool isPlayerBoostOver;
    bool isBotBoostOver;
    int fighterSortingOrder = -1;

    //Entrypoint for user
    public void OnClickTriggerBoostEffects()
    {
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
        SetIsBoostOverValue(fighter, false);

        Animator lightningAnimator = fighter.transform.Find("VFX/Boost_VFX/Lightning_VFX").GetComponent<Animator>();
        lightningAnimator.Play("lightning_0", -1, 0f);
        fighter.damage *= 1.5f;
        fighter.GetComponent<Renderer>().material.color = new Color32(255, 192, 0, 255);

        Combat.player.GetComponent<Renderer>().sortingOrder = 1;
        Combat.bot.GetComponent<Renderer>().sortingOrder = 1;

        StartCoroutine(StartBoostTimer(fighter));
        StartCoroutine(ShowParticlesWhileBoostLast(fighter));
    }

    private void SetIsBoostOverValue(Fighter fighter, bool isBoostOver){
        if (Combat.player == fighter) isPlayerBoostOver = isBoostOver;
        else isBotBoostOver = isBoostOver;
    }


    IEnumerator StartBoostTimer(Fighter fighter)
    {
        yield return new WaitForSeconds(boostDuration);
        SetIsBoostOverValue(fighter, true);
    }

    IEnumerator ShowParticlesWhileBoostLast(Fighter fighter)
    {
        while (!IsBoostOver(fighter))
        {
            StartParticlesAnimation(fighter);
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(RemoveBoostEffects(fighter));
    }

    IEnumerator RemoveBoostEffects(Fighter fighter)
    {
        fighter.damage /= 1.5f;
        fighter.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        //Wait for particles animation to finish
        yield return new WaitForSeconds(1f);
        ResetFightersSortingOrder(fighterSortingOrder);
    }

    private void ResetFightersSortingOrder(int sortingOrder)
    {
        //Only reset sorting order if no fighters have an active boost
        //Null: Not used yet, False: Boost in progress, True: Boost finished
        if (isPlayerBoostOver != false && isBotBoostOver != false)
        {
            Combat.player.GetComponent<Renderer>().sortingOrder = sortingOrder;
            Combat.bot.GetComponent<Renderer>().sortingOrder = sortingOrder;
        }
    }

    private bool IsBoostOver(Fighter fighter)
    {
        return Combat.player == fighter ? isPlayerBoostOver : isBotBoostOver;
    }
}