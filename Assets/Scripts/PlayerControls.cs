using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    public bool DialougeOpen = false;
    [Header("Properties")]
    [SerializeField] float force;
    [SerializeField] LayerMask mask;
    [SerializeField] float sensitivity = 1f;
    [SerializeField] Vector3 camOffset;
    [SerializeField] float jumpforce;
    [SerializeField] float gravity;
    [SerializeField] float friction;
    [SerializeField] float knockbackFriction;
    [SerializeField] float staticFriction;
    [SerializeField] float offset;
    [SerializeField] float camSmoothTime = 0.1f;
    Vector3 lastCamRot;
    Vector3 camRotTarget;
    Smoothing s;
    [SerializeField] float forwardDelta = 90f;
    [SerializeField] float spdMult = .01f;
    [SerializeField] LayerMask interactionMask;
    [SerializeField] float maxKnockbackTime;
    [SerializeField] float knockbackForce;
    [SerializeField] float mouseSensitivity;
    float knockbackTime = 0f;
    Vector2 camAngle;
    Vector2 currentAngle = Vector2.zero;
    PlayerActions move;
    Vector3 velLastFrame;
    float lastDelta = 0f;

    [Header("Prefabs")]
    [SerializeField] GameObject blueBullet;
    [SerializeField] GameObject greenBullet;
    [SerializeField] GameObject redBullet;
    [SerializeField] GameObject yellowBullet;
    [SerializeField] GameObject triBullet;
    [SerializeField] GameObject absorptionBullet;
    CharacterActor actor;

    [Header("Object References")]
    [SerializeField] Camera cam;
    [SerializeField] GameObject body;
    [SerializeField] GameObject visual;
    [SerializeField] Animator anim;
    [SerializeField] PlayerBulletManager playerBulletManager;
    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] ParticleSystem part;
    [SerializeField] Transform playerCenter;
    [SerializeField] FindNearestEnemy find;
    [SerializeField] GameObject ControlsUI;
    [SerializeField] PauseManager pause;
    Rigidbody rb;
    bool controlsOpen = false;
    bool canStop => _canStop();
    bool _canStop(){return knockbackTime<=0;}

    bool invincible = false;
    public void SetInvincibility(bool b) { invincible = b; }

    void Start()
    {
        actor = GetComponent<CharacterActor>();
        move = new PlayerActions();
        move.Enable();
        camAngle = new Vector2(0f, 0f);
        velLastFrame = Vector3.zero;
        Cursor.lockState=CursorLockMode.Locked;
        ControlsUI.SetActive(controlsOpen);
        rb = GetComponent<Rigidbody>();
    }

    #region triggered from animations
    public void Fire(){
        GameObject toFire = redBullet;
        Bullet firing = playerBulletManager.ShootPrimary();
        if(firing == null)
        {
            return;
        }
        switch (firing.GetBulletType())
        {
            case Bullet.bulletTypes.Blue:
                toFire = blueBullet;
                break;
            case Bullet.bulletTypes.Green:
                toFire = greenBullet;
                break;
            case Bullet.bulletTypes.Red:
                toFire = redBullet;
                break;
            case Bullet.bulletTypes.Yellow:
                toFire = yellowBullet;
                break;
        }
        FireBullet(toFire);
    }

    public void FireTriBullet()
    {
        FireBullet(triBullet);
    }

    public void FireAbsorptionBullet()
    {
        FireBullet(absorptionBullet);
    }
    private void FireBullet(GameObject bulletType)
    {
        GameObject pb;
        if (find.closestEnemy == null)
        {
            pb = Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.Euler(-currentAngle.y, currentAngle.x, 0f));
        }
        else
        {
            pb = Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.LookRotation(find.closestEnemy.transform.position - bulletSpawnPoint.transform.position, Vector3.up));
        }

        pb.GetComponent<PlayerBullet>().SetDamage(playerBulletManager.GetDamageMult());
        Debug.Log(pb.GetComponent<PlayerBullet>().GetDamage);
    }

    public void Phantomize()
    {
        Bullet firing = playerBulletManager.PhantomizeBullets();

        if(firing.GetBulletType() != Bullet.bulletTypes.White)
        {
            var main = part.main;
            main.startColor = firing.GetColor();
            part.Play();
        }
    }
    #endregion
    private void Update()
    {
        //this could be replaced w a delegate or smth for later but with adding and removing objects this is faster for now
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (PauseManager.IsPaused)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            anim.speed = 0f;
            return;
        }
        else
        {
            anim.speed = 1f;
            rb.constraints = RigidbodyConstraints.None;
        }

        float delta = Mathf.Atan2(velLastFrame.z, velLastFrame.x) * Mathf.Rad2Deg;

        if (s != null)
        {
            
            float t = s.TickVal(Time.deltaTime);
            currentAngle = Vector3.Lerp(lastCamRot, camRotTarget, t);
            if (t >= 1)
            {
                s = null;
            }
        }
        else
        {
            currentAngle = camRotTarget;
        }
        float extraOffset = 0f;
        
        Vector2 input = move.Main.movement.ReadValue<Vector2>();
        float horiz = input.x/2f+.5f;
        anim.SetFloat("Horizontal Direction",horiz);
        anim.SetFloat("Spd",actor.PlanarVelocity.magnitude * spdMult * Mathf.Min(move.Main.movement.ReadValue<Vector2>().magnitude,1));

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
                lastDelta = currentAngle.x - 90f;// Mathf.LerpAngle(lastDelta,currentAngle.x -90f, deltaLerp) ;
                anim.SetBool("Horiz",true);
            }else{
                lastDelta = -delta + extraOffset;// Mathf.LerpAngle(lastDelta, -delta + extraOffset, deltaLerp) ;
                anim.SetBool("Horiz",false);
            }
        }
        else
        {
            lastDelta = currentAngle.x - 90f;//Mathf.LerpAngle(lastDelta, currentAngle.x - 90f, deltaLerp / 2f);
            anim.SetBool("Horiz", false);
        }
        body.transform.rotation = Quaternion.Euler(-90f, lastDelta + offset, 0f);
        visual.transform.rotation = Quaternion.Euler(-currentAngle.y, currentAngle.x, 0f);
        if(knockbackTime>0){
            knockbackTime = Mathf.Max(0f,knockbackTime-Time.deltaTime);
        }

        Vector2 v = move.Main.cameraMovement.ReadValue<Vector2>();
        if (v.magnitude != 0f)
        {
            if (move.Main.cameraMovement.activeControl.displayName == "Delta")
            {
                //v.x *= Screen.width;
                // v.y *= Screen.height;
                v *= mouseSensitivity * 100 ;
            }
            else
            {
                v *= sensitivity;
            }
            //Debug.Log(move.Main.cameraMovement.activeControl.displayName);
            
        }

        Vector2 c = camAngle;

        if (!DialougeOpen)
        {

            camAngle += v;
            camAngle.y = Mathf.Clamp(camAngle.y, -85f, 85f);

        }
        if (camAngle != c)
        {
            s = new Smoothing(0f, camSmoothTime, Smoothing.smoothingTypes.InFastOutSlow);
            lastCamRot = currentAngle;
            camRotTarget = camAngle;
            //Debug.Log("new s");
        }

    }


    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag != "Bullet")
            return;

        if (!invincible)
        {
            Debug.Log("hit bullet");
            knockbackTime = maxKnockbackTime;
            actor.Velocity = -(collision.gameObject.transform.position - playerCenter.position).normalized * knockbackForce;
            playerBulletManager.notHit = false;
        }
        Destroy(collision.gameObject);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //y is horiz
        //x is vert
        //Vector2 angle
        if (PauseManager.IsPaused) { return; }
        


        
        //Debug.Log(forward);
        //Debug.Log(right);
        Vector3 movement = new Vector3(0f, 0f, 0f);
        if(!DialougeOpen){
            
            Vector2 input = move.Main.movement.ReadValue<Vector2>();
            //Debug.Log(forward);
            //Debug.Log(right);
            
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;

            forward.y = 0f;
            right.y = 0f;
            right.Normalize();
            forward.Normalize();
            movement += forward * input.y;
            movement += right * input.x;
            movement *= force;
        }
        
        //Debug.Log(movement);
        //Debug.DrawRay(transform.position,movement);
        if (actor.IsGrounded)
        {
            actor.Velocity += movement * (1f-knockbackTime/maxKnockbackTime);
            if(canStop){
                actor.PlanarVelocity *= friction;
            }else{
                actor.PlanarVelocity *= knockbackFriction;
            }
            
            if (actor.PlanarVelocity.magnitude < staticFriction && canStop)
            {
                actor.PlanarVelocity = Vector3.zero;
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
        RaycastHit hit;
        Physics.Raycast(transform.position, visual.transform.forward, out hit, interactionMask );
        if (hit.collider != null)
        {
            //Debug.Log("Hit collider");
        }
    }
    #region input system actions
    public void Jump (InputAction.CallbackContext ctx)
    {
        if (PauseManager.IsPaused) { return; }
        if (ctx.started && !DialougeOpen)
        {
            actor.ForceNotGrounded();
            actor.VerticalVelocity += new Vector3(0f, jumpforce, 0f);
        }
    }
    public void Net(InputAction.CallbackContext ctx)
    {
        if (PauseManager.IsPaused) { return; }
        if (ctx.started && !DialougeOpen)
        {
            anim.SetTrigger("Swing");
        }
    }
    public void Fire(InputAction.CallbackContext ctx)
    {
        if (PauseManager.IsPaused) { return; }
        if (ctx.started && !DialougeOpen)
        {
            anim.SetTrigger("Shoot");
        }
    }
    public void Buff(InputAction.CallbackContext ctx){
        if (PauseManager.IsPaused) { return; }
        if (ctx.started && !DialougeOpen){
            anim.SetTrigger("Buff");
        }
    }
    public void Controls(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            controlsOpen = !controlsOpen;
            ControlsUI.SetActive(controlsOpen);
            pause.SetPaused(controlsOpen);
        }
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            controlsOpen = false;
            ControlsUI.SetActive(controlsOpen);
            pause.TogglePaused();
        }
    }
    #endregion

    public void CloseControls()
    {
        controlsOpen = false;
        ControlsUI.SetActive(controlsOpen);
        pause.SetPaused(controlsOpen);
    }
}
