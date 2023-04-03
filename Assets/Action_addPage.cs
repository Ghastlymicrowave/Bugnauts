using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_addPage : ActionBase
{
    [SerializeField] FieldGuideScriptableObject page;
    [SerializeField] FieldGuide guide;
    public override void Activate()
    {
        base.Activate();
        guide.AddPage(page);
    }
}
