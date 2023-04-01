using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCinematicTrigger : ActionBase
{
    GameTracker tracker;
    [SerializeField] string triggerID;
    bool triggered;
    [SerializeField] bool oneTimeOnly;
    [SerializeField] bool canActivateOutOfOrder;
    private void Start()
    {
        tracker = GameObject.Find("Canvas").GetComponent<GameTracker>();
    }
    public override void Activate()
    {
        base.Activate();

        if(oneTimeOnly)
        {
            if (!triggered)
            {
                triggered = tracker.SendCustomTrigger(triggerID, canActivateOutOfOrder);
                Debug.Log("triggered: " + triggerID);
            }
            else
            {
                tracker.SendCustomTrigger(triggerID, canActivateOutOfOrder);
                Debug.Log("triggered: " + triggerID);
            }
        }
        else
        {
            tracker.SendCustomTrigger(triggerID, canActivateOutOfOrder);
            Debug.Log("triggered: " + triggerID);
        }
    }
}
