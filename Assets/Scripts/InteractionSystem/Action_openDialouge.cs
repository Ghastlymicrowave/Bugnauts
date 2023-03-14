using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_openDialouge : ActionBase
{
    [SerializeField]Dialogue_scriptableObj text;
    [SerializeField] List<ActionBase> endActions;
    float startWait = 0.01f;
    float waitTime = 0;
    public override void Activate()
    {
        base.Activate();
        PlayerControls p = GameObject.Find("Player").GetComponent<PlayerControls>();
        if(p!=null){
            p.DialougeOpen = true;

            
            waitTime = startWait;
        }
    }

    void Update(){
        if(waitTime>0){
            waitTime-=Time.deltaTime;
            if(waitTime<=0){
                waitTime=0;
                GameObject.Find("Canvas").GetComponent<DialogueSystem>().StartDialogue(text,this);
            }
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
