using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimationAction : ActionBase
{
    [SerializeField] Animation anim;
    public override void Activate()
    {
        base.Activate();
        anim.Play();
    }
}
