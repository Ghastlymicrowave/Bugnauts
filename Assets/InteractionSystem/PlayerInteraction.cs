using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    //checks for interactables on layer: default with tag: interactable
    [SerializeField] Transform headTransform;
    [SerializeField] float interactionStartDist;
    [SerializeField] float interactionEndtDist;
    [SerializeField] float sphereRadius;
    [SerializeField] LayerMask raycastMaskInteract;
    [SerializeField] LayerMask raycastWorld;
    [SerializeField] Image handUI;
    [SerializeField] float smoothTime;
    
    float lastOpacity = 0f;
    float targetOpacity = 0f;
    float currentOpaicty = 0f;
    public bool canInteract = true;
    Smoothing handUISmooth;

    //[SerializeField] SoundEffector sfx;
    //public static InventoryManager Inventory;
    public InteractableBase.interactType HoveringIntType => GetHoverType();
    InteractableBase.interactType GetHoverType(){
        if (hovering==null){
            return InteractableBase.interactType.empty;
        }
        return hovering.GetIntType();
    }
    public bool CurrentlyInteracting => currentlyInteracting;
    bool currentlyInteracting = false;
    InteractableBase hovering;
    Collider hoveringCollider;
    public static PlayerInteraction player;
    void Awake(){
        player = this;
    }

    private void Start()
    {
        handUI.color = new Color(1f, 1f, 1f, 0f);
    }

    public InteractableBase GetHovering()
    {
        return hovering;
    }
    public Collider CurrentGrabHitbox()
    {
        return hoveringCollider;
    }

    public void InteractPressed(InputAction.CallbackContext obj)
    {
        Debug.Log("proc");
        if(obj.canceled){
            InteractReleased();
            return;
        }
        if(obj.started){
        if (!canInteract) { return; }
        if (GetHoverType() == InteractableBase.interactType.press){
            hovering.Interact(true);
            Debug.Log("interact");
        }else if (GetHoverType() != InteractableBase.interactType.empty){
            currentlyInteracting = true;
        }
        }
        
    }
    public void InteractReleased()
    {
        if (hovering != null)
        {
            hovering.StopGrabbing(); 
        }
        currentlyInteracting = false;
    }
    public void ResetHover()
    {
        if (hovering != null)
        {
            hovering.StopGrabbing();
        }
        currentlyInteracting = false;
        hovering = null;
    }
    private void NoCollide()
    {
        if (!currentlyInteracting)
        {
            hovering = null;
        }
    }
    void FixedUpdate()
    {
        Collider[] info;
        info = Physics.OverlapCapsule(headTransform.position + headTransform.transform.forward * interactionStartDist,headTransform.position + headTransform.transform.forward * interactionEndtDist, sphereRadius,raycastMaskInteract);
        for(int i = 0; i < info.Length; i ++){
            Debug.DrawLine(headTransform.position, info[i].ClosestPoint(headTransform.position), Color.red, 0.1f);
            if (hovering == null || (hovering.gameObject != info[i].gameObject))
            {
                RaycastHit test;
                Vector3 headPos = headTransform.position;
                Vector3 colPos = info[i].ClosestPoint(headTransform.position);
                Physics.Raycast(colPos, headPos - colPos, out test, Vector3.Distance(headPos, colPos), raycastWorld);
                if (test.collider == null || test.collider.gameObject == info[i].gameObject)//
                {
                    if (!currentlyInteracting)
                    {
                        hovering = info[i].gameObject.GetComponent<InteractableBase>();
                        hoveringCollider = info[i];
                    }
                    Debug.Log("hovering");
                    if (targetOpacity != 1f)
                    {
                        targetOpacity = 1f;
                        lastOpacity = currentOpaicty;
                        handUISmooth = new Smoothing(0f, smoothTime, Smoothing.smoothingTypes.InFastOutSlow);
                    }
                    goto found;
                }
            }
        }
        if (info.Length<1){
            NoCollide();
            if (targetOpacity != 0f)
            {
                targetOpacity = 0f;
                lastOpacity = currentOpaicty;
                handUISmooth = new Smoothing(0f, smoothTime, Smoothing.smoothingTypes.InFastOutSlow);
            }
        }
        
    found:;
    }

    void OnDrawGizmos(){
        Gizmos.DrawSphere(headTransform.position + headTransform.transform.forward * interactionEndtDist, sphereRadius);
        Gizmos.DrawSphere(headTransform.position + headTransform.transform.forward * interactionStartDist, sphereRadius);
    }

    

    private void Update()
    {
        if (handUISmooth != null)
        {
            float t = handUISmooth.TickVal(Time.deltaTime);
            currentOpaicty = Mathf.Lerp(lastOpacity, targetOpacity, t);
            handUI.color = new Color(1f, 1f, 1f, currentOpaicty);
            if (t >= 1f)
            {
                handUISmooth = null;
            }
        }
    }
}
