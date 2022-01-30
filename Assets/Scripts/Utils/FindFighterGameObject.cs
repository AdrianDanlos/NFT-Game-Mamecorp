using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindFighterGameObject
{
    public static GameObject Find(){
        return GameObject.Find("FighterWrapper").transform.Find("Fighter").gameObject;
    }
}
