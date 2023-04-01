using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFieldGuideAction : ActionBase
{
    [SerializeField] PlayerControls pc;
    public override void Activate()
    {
        base.Activate();
        pc.OpenGuide();
    }
}
