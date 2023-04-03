using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDisablePlayerInputExceptForKey : ActionBase
{
    [SerializeField]PlayerControls pc;
    [SerializeField] PlayerControls.SingleKey key;
    public override void Activate()
    {
        base.Activate();
        pc.SetSingleKey(key);
    }
}
