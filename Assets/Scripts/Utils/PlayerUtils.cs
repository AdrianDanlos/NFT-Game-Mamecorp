using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUtils
{
    public static GameObject FindInactiveFighterGameObject()
    {
        return GameObject.Find("FighterWrapper").transform.Find("Fighter").gameObject;
    }
}
