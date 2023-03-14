using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Trigger : InteractableBase
{
    [SerializeField] bool triggerOnce;
    [SerializeField] Color color;
    Collider col;
    bool triggered;

    void Awake(){
        intType = interactType.empty;
    }

    void Start(){
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider col){
        if(col.tag == "Player"){
            if(triggerOnce){
                if(!triggered){
                    Activate();
                    triggered = true;
                }
            }
            else{
                Activate();
            }
        }
    }

    void Activate(){
        if(actions != null){
            for(int i = 0; i < actions.Count; i++){
                actions[i].Activate();
            }
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = color;
        if(col == null){
            col = GetComponent<Collider>();
        }
        if(col == null){
            return;
        }
        if(col.GetType() == typeof(BoxCollider)){
            BoxCollider box = (BoxCollider)col;
            Gizmos.DrawCube(box.bounds.center,col.bounds.size);
        }
    }
}
