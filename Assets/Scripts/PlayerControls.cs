using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using UnityEngine.InputSystem;
using Cinemachine;
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
    [SerializeField] Vector2 currentAngle = Vector2.zero;
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
    [SerializeField] CinemachineVirtualCamera fieldGuideCam;

    [SerializeField] CustomCinematicTrigger guideCloseTrigger;
    [SerializeField] GameObject fieldGuideButtons;
    [SerializeField] FieldGuide guide;
    Vector3 currentRot;

    Smoothing smoothTo0;

    bool fieldGuideOpen = false;
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
        smoothTo0 = new Smoothing(1f, 1f, Smoothing.smoothingTypes.InFastOutSlow);
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
            if (playerBulletManager.GetMultiShot())
            {
                Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.Euler(-currentAngle.y, currentAngle.x + 15, 0f));
                Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.Euler(-currentAngle.y, currentAngle.x - 15, 0f));
            }
        }
        else
        {
            pb = Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.LookRotation(find.closestEnemy.transform.position - bulletSpawnPoint.transform.position, Vector3.up));
            if (playerBulletManager.GetMultiShot())
            {
                Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.LookRotation(find.closestEnemy.transform.position - bulletSpawnPoint.transform.position, Vector3.up) * Quaternion.Euler(0f, 15f, 0f));
                Instantiate(bulletType, bulletSpawnPoint.transform.position, Quaternion.LookRotation(find.closestEnemy.transform.position - bulletSpawnPoint.transform.position, Vector3.up) * Quaternion.Euler(0f, -15f, 0f));
            }
        }

        pb.GetComponent<PlayerBullet>().SetDamage(playerBulletManager.GetDamageMult());
        pb.GetComponent<PlayerBullet>().SetSpeed(playerBulletManager.GetSpeedMult());
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
            if (PauseManager.stopAnims)
            {
                anim.speed = 0f;
                visual.transform.rotation = Quaternion.Euler(new Vector3(-currentAngle.y, currentAngle.x, 0f));
            }
            else
            {
                anim.speed = 1f;

                float t1 = smoothTo0.TickVal(Time.deltaTime);
                Vector3 r1 = Vector3.Lerp(new Vector3(-currentAngle.y, currentAngle.x, 0f), new Vector3(0f, currentAngle.x, 0f), t1);
                visual.transform.rotation = Quaternion.Euler(r1);
            }
            
            return;
        }
        else
        {
            anim.speed = 1f;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        float delta = Mathf.Atan2(velLastFrame.z, velLastFrame.x) * Mathf.Rad2Deg;

        if (s != null)
        {
            
            float t3 = s.TickVal(Time.deltaTime);
            
            currentAngle = new Vector3(Mathf.LerpAngle(lastCamRot.x, camRotTarget.x, t3), Mathf.LerpAngle(lastCamRot.y, camRotTarget.y, t3), Mathf.LerpAngle(lastCamRot.z, camRotTarget.z, t3));
            if (t3 >= 1)
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

        float t = smoothTo0.TickVal(Time.deltaTime);
        Vector3 r = Vector3.Lerp(new Vector3(0f, currentAngle.x, 0f), new Vector3(-currentAngle.y, currentAngle.x, 0f), t);
        visual.transform.rotation = Quaternion.Euler(r);

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
            while (camAngle.x > 360f)
            {
                camAngle.x -= 360f;
            }
            while (camAngle.x < 0f)
            {
                camAngle.x += 360f;
            }

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
        if (collision.gameObject.tag == "Bullet" && !invincible)
        {
            Debug.Log("hit bullet");
            knockbackTime = maxKnockbackTime;
            actor.Velocity = -(collision.gameObject.transform.position - playerCenter.position).normalized * knockbackForce;
            playerBulletManager.notHit = false;
        }
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
            
            Vector3 forward = visual.transform.forward;
            Vector3 right = visual.transform.right;

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
            PauseManager.SetPaused(controlsOpen);
        }
    }
    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.started && PauseManager.playerCanUnpause)
        {
            controlsOpen = false;
            ControlsUI.SetActive(controlsOpen);
             
            PauseManager.stopAnims = true;
            PauseManager.showUI = true;
            PauseManager.TogglePaused();
        }
    }
    public void OpenFieldGuide(InputAction.CallbackContext ctx)
    {
        if (ctx.started && PauseManager.playerCanUnpause)
        {
            fieldGuideOpen = !fieldGuideOpen;
            if (fieldGuideOpen)
            {
                PauseManager.stopAnims = false;
                PauseManager.showUI = false;
            }
            else
            {
                PauseManager.showUI = true;

            }
            PauseManager.SetPaused(fieldGuideOpen);

            anim.SetBool("GuideOpen", fieldGuideOpen);
            if (fieldGuideOpen)
            {
                fieldGuideCam.Priority = 15;
                anim.Play("Open", 1);
                anim.Play("null", 0);
                anim.SetFloat("Spd", 0f);
                
            }
            else
            {
                fieldGuideCam.Priority = 0;
                guideCloseTrigger.Activate();
            }
            smoothTo0 = new Smoothing(0f, 1f, Smoothing.smoothingTypes.InFastOutSlow);
            PauseManager.SetReticleEnabled(!fieldGuideOpen);
            fieldGuideButtons.SetActive(fieldGuideOpen);
            guide.DisplayPage();
        }
    }
    #endregion

    public void CloseControls()
    {
        controlsOpen = false;
        ControlsUI.SetActive(controlsOpen);
        PauseManager.SetPaused(controlsOpen);
    }

    public void OpenGuide()
    {
        fieldGuideOpen = true;
        PauseManager.stopAnims = false;
        PauseManager.showUI = false;
        PauseManager.SetPaused(true);

        anim.SetBool("GuideOpen", fieldGuideOpen);
        fieldGuideCam.Priority = 15;
        anim.Play("Open", 1);
        anim.Play("null", 0);
        anim.SetFloat("Spd", 0f);
        smoothTo0 = new Smoothing(0f, 1f, Smoothing.smoothingTypes.InFastOutSlow);
        PauseManager.SetReticleEnabled(!fieldGuideOpen);
        fieldGuideButtons.SetActive(fieldGuideOpen);
        guide.DisplayPage();
    }
}
