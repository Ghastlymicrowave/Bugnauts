using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    CharacterActor actor;
    [SerializeField] float force;
    [SerializeField] Camera cam;
    [SerializeField] LayerMask mask;
    [SerializeField] GameObject body;
    [SerializeField] GameObject visual;
    [SerializeField] float sensitivity = 1f;
    [SerializeField] Vector3 camOffset;
    [SerializeField] float jumpforce;
    [SerializeField] float gravity;
    [SerializeField] float friction;
    [SerializeField] float staticFriction;
    [SerializeField] float offset;
    [SerializeField] Animator anim;
    Vector3 visualPosition;
    Vector2 camAngle;
    Vector2 currentAngle = Vector2.zero;
    // Start is called before the first frame update
    PlayerActions move;
    Vector3 velLastFrame;
    float lastDelta = 0f;
    [SerializeField] float deltaLerp = 0.1f;
    [SerializeField]float forwardDelta = 90f;
    [SerializeField] float spdMult =.01f;

    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] GameObject redBullet;


    void Start()
    {
        actor = GetComponent<CharacterActor>();
        move = new PlayerActions();
        move.Enable();
        camAngle = new Vector2(0f, 0f);
        velLastFrame = Vector3.zero;
        Cursor.lockState=CursorLockMode.Locked;
    }

    public void Fire(){
        GameObject pb = Instantiate(redBullet,bulletSpawnPoint.transform.position, Quaternion.Euler(-currentAngle.y, currentAngle.x, 0f));
    }

    private void Update()
    {
        //camRotater.transform.position = Vector3.Lerp(camRotater.transform.position, transform.position + camOffset,0.2f);
        //visualPosition = Vector3.Lerp(visualPosition, transform.position, 0.2f);
        
        //body.transform.rotation = Quaternion.Euler(0f,currentAngle.x,0f);
        //x 90 90
        float delta = Mathf.Atan2(velLastFrame.z, velLastFrame.x) * Mathf.Rad2Deg;
        
        currentAngle = Vector2.Lerp(currentAngle, camAngle, 0.1f);
        
        float extraOffset = 0f;
        
        Vector2 input = move.Main.movement.ReadValue<Vector2>();
        float horiz = input.x/2f+.5f;
        anim.SetFloat("Horizontal Direction",horiz);
        anim.SetFloat("Spd",actor.Velocity.magnitude * spdMult);


        float a = -delta + offset;
        float b = currentAngle.x;
        if(Mathf.Abs(Mathf.DeltaAngle(a,b))>90f){
            extraOffset = 180f;
            
            //anim.SetFloat("WalkDirection",-1f);
        }else{
            //anim.SetFloat("WalkDirection",1f);
        }


        if (input.magnitude != 0f)
        {
            
            if(Mathf.Abs(input.normalized.x)>.75f){
                lastDelta = Mathf.LerpAngle(lastDelta,currentAngle.x -90f, deltaLerp) ;
                anim.SetBool("Horiz",true);
            }else{
                lastDelta = Mathf.LerpAngle(lastDelta, -delta + extraOffset, deltaLerp) ;
                anim.SetBool("Horiz",false);
            }
        }
        
        body.transform.rotation = Quaternion.Euler(-90f, lastDelta + offset, 0f);

        
        
        visual.transform.rotation = Quaternion.Euler(-currentAngle.y, currentAngle.x, 0f);

        
        
        

    }


    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag=="Bullet"){
            Debug.Log("hit bullet");
            //knockback
            //take damage
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        
        //y is horiz
        //x is vert
        //Vector2 angle
        camAngle += move.Main.cameraMovement.ReadValue<Vector2>() * sensitivity;
        camAngle.y = Mathf.Clamp(camAngle.y, -85f, 85f);
        

        Vector2 input = move.Main.movement.ReadValue<Vector2>();

        //Debug.Log(forward);

        //Debug.Log(right);

        Vector3 movement = new Vector3(0f, 0f, 0f);
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        movement += forward * input.y;
        movement += right * input.x;
        movement *= force;
        
        //Debug.Log(movement);
        Debug.DrawRay(transform.position,movement);
        if (actor.IsGrounded)
        {
            actor.Velocity += movement;
            actor.Velocity *= friction;
            if (actor.Velocity.magnitude < staticFriction)
            {
                actor.Velocity = Vector3.zero;
            }
        }
        else
        {
            actor.VerticalVelocity += -transform.up * gravity;
        }
        if (movement.magnitude > 0f)
        {
            velLastFrame = movement;
        }
        
    }

    public void Jump (InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            actor.ForceNotGrounded();
            actor.VerticalVelocity += new Vector3(0f, jumpforce, 0f);
        }
    }
    public void Net(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            anim.SetTrigger("Swing");
        }
    }
    public void Fire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            anim.SetTrigger("Shoot");
        }
    }
}
