using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Button : InteractableBase
{
    [SerializeField] List<ActionBase> actions;
    void Awake(){
        intType = interactType.press;
    }
    public override void Interact(bool startedgrabbing = false)
    {
        Debug.Log("triggered");
        if(actions != null){
            for(int i = 0; i < actions.Count; i++){
                actions[i].Activate();
            }
        }
    }
}
