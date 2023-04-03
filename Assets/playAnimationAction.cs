using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimationAction : ActionBase
{
    [SerializeField] List<Animation> anim;
    public override void Activate()
    {
        base.Activate();
        for(int i = 0; i < anim.Count; i++)
        {
            anim[i].Play();
        }
        
    }
}
