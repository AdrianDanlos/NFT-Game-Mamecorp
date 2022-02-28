using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUtils
{
    public static Fighter FindInactiveFighter()
    {
        GameObject fighterGameObject = GameObject.Find("FighterWrapper").transform.Find("Fighter").gameObject;
        Fighter fighter = fighterGameObject.GetComponent<Fighter>();
        return fighter;
    }

    public static GameObject FindInactiveFighterGameObject() 
    {
        //FIXME: Reuse this with the call above
        return GameObject.Find("FighterWrapper").transform.Find("Fighter").gameObject;
    }
}
