using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    public enum interactType
    {
        press,//short tap or press of E??
        hold,//long press, with movement optional
        empty//null
    }
    protected interactType intType;

    //must be set in class :
    /*
    void Awake(){
        intType = interactType.press_short;
    }
    */

    public interactType GetIntType()
    {
        return intType;
    }
    public virtual void Interact(bool startedgrabbing = false)
    {
        //Interaction to be triggered on long/short press
    }
    public virtual void StopGrabbing()
    {
        
    }
    public virtual void GrabbingInteract(Vector2 input)
    {
        //interaction to be triggered while holding (meant for grabbing and dragging, prob going to be unused)
    }
}
