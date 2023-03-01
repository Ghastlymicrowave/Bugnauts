using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimAction : ActionBase
{
    bool talking = false;
    [SerializeField] Animator anim;
    public override void Activate()
    {
        base.Activate();
        talking = !talking;
        anim.SetBool("talking",talking);
    }
}
