using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_openDialouge : ActionBase
{
    [SerializeField]Dialogue_scriptableObj text;
    [SerializeField] List<ActionBase> endActions;
    public override void Activate()
    {
        base.Activate();
        PlayerControls p = GameObject.Find("Player").GetComponent<PlayerControls>();
        if(p!=null){
            p.DialougeOpen = true;
            GameObject.Find("Canvas").GetComponent<DialogueSystem>().StartDialogue(text,this);
        }
    }

    public void Trigger(){
        if(endActions != null){
            for(int i = 0; i < endActions.Count; i++){
                endActions[i].Activate();
            }
        }
    }
}
