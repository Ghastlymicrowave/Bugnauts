using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : MonoBehaviour
{
    public virtual void Activate(){
        Debug.Log("Interaction triggered, obj: "+gameObject.name+" script: "+this.name);
    }
}
