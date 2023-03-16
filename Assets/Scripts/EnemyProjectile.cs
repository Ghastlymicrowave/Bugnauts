using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] Bullet storedBullet;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject Graphics;
    [SerializeField] float fadeoutTime;
    public Bullet Bullet => storedBullet;
    float lifetime;
    float StartingLifetime;

    [SerializeField] Vector3 localTranslateScale;
    [SerializeField] Curves translateCurve;
    [SerializeField] float LoopsPerLifetime;
    [SerializeField] Vector3 localTranslateOffset;
    Vector3 graphicsStartScale;
    float loopLength() {
        return StartingLifetime / LoopsPerLifetime;
    }

    bool taggedForDeletion = false;

    //[SerializeField]List<BulletEvent> events;
    //[SerializeField]int eventIndex = 0;
    //[SerializeField]float eventTimer = 0f;

    //float speed_last;
    //Quaternion rotation_last;
    //Quaternion initalRotation;

    private void Start()
    {
        graphicsStartScale = Graphics.transform.localScale;
    }

    public enum Curves
    {
        SIN, //full sine wave per loop
        ABS_SIN //2 bounces per loop
    }

    public void SetLifetime(float l)
    {
        lifetime = l;
        StartingLifetime = l;
        
    }

    public void Translate()
    {
        if (!Graphics)
        {
            return;
        }
        Vector3 offset = localTranslateOffset;
        float t = StartingLifetime - lifetime;
        float angl = 2f * t * Mathf.PI / loopLength();
        switch (translateCurve)
        {
            case Curves.SIN:
                offset += localTranslateScale * Mathf.Sin(angl);
                break;
            case Curves.ABS_SIN:
                offset += localTranslateScale * Mathf.Abs(Mathf.Sin(angl));
                break;
        }
        Graphics.transform.localPosition = offset;
        if(offset != Vector3.zero)
        {
            //Debug.Log(localTranslateScale * Mathf.Abs(Mathf.Sin(angl)));
        }
        
    }

    public void SetTranslate(Vector3 scl, Curves c, float loops, Vector3 offset)
    {
        localTranslateScale = scl;
        translateCurve = c;
        LoopsPerLifetime = loops ;
        localTranslateOffset = offset;
        Debug.Log(loopLength());
        Debug.Log(StartingLifetime);
        Debug.Log(LoopsPerLifetime);
    }


    Vector3 ang;
    float spd;
    public void SetStart(float nspd, Vector3 angVel)
    {
        ang = angVel;
        spd = nspd;
    }

    void FixedUpdate()
    {
        if (PauseManager.IsPaused) {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        transform.Rotate(ang);
        rb.velocity = transform.forward * spd;
    }

    void Update()
    {
        if (PauseManager.IsPaused) { return; }
        lifetime -= Time.deltaTime;
        Translate();
        if (lifetime <= 0 && !taggedForDeletion && Graphics)
        {
            taggedForDeletion = true;
            Graphics.GetComponent<Collider>().enabled = false;
            Invoke("Kill", fadeoutTime);
        }

        if (taggedForDeletion && Graphics)
        {
            Graphics.transform.localScale = Vector3.Lerp(graphicsStartScale, Vector3.zero, -lifetime / fadeoutTime);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    //[SerializeField]float currentSpeed;
    /*
    public void SetStartingSpeed(float sp){
        currentSpeed = sp;
        speed_last = sp;
    }
    public void SetStartingRot(){
        initalRotation = transform.rotation;
        rotation_last = transform.rotation;
    }
   
    void Start()
    {
        //initalRotation = transform.rotation;
        //rotation_last = transform.rotation;
        //speed_last = currentSpeed;
        //GetComponent<Renderer>().material.color = storedBullet.GetColor();  
        //rb = GetComponent<Rigidbody>();
    }

    
    
    BulletEvent CurrentEvent(){
        if(eventIndex>events.Count-1){
            return null;
        }
        return events[eventIndex];
    }
    float GetTime(){
        if(CurrentEvent().transitionTime==0f || eventTimer>CurrentEvent().transitionTime){
            return 1f;
        }
        return eventTimer/CurrentEvent().transitionTime;
    }
    float time => GetTime();
    void Update()
    {
        eventTimer+=Time.deltaTime;
        SetRotation();
        SetSpeed();

        if(eventTimer > CurrentEvent().transitionTime){

            if(CurrentEvent().ChangeRotation){
                rotation_last=transform.rotation;
            }
            if(CurrentEvent().ChangeSpeed){
                speed_last = currentSpeed;
            }
            ParseFlags(CurrentEvent().endFlags);
            NextEvent();
            eventTimer = 0f;
        }

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }
    void SetRotation(){
        if(CurrentEvent().ChangeRotation){
                Quaternion q;
                if(CurrentEvent().rotationIsRelatativeToLastRotation){
                    q = Quaternion.Euler(CurrentEvent().EulerRotation + rotation_last.eulerAngles);
                }else{
                    q = Quaternion.Euler(CurrentEvent().EulerRotation);
                }
                q = Quaternion.Lerp(rotation_last,q,time);
                transform.rotation = q;
            }
    }
    void SetSpeed(){
        if(CurrentEvent().ChangeSpeed){
            float s = speed_last;
            if(CurrentEvent().speedChangeIsRelativeToLastSpeed){
                s = Mathf.Lerp(s,s+CurrentEvent().speed,time);
            }else{
                s = Mathf.Lerp(s,CurrentEvent().speed,time);
            }
            currentSpeed = s;
        }
    }
    void NextEvent(){
        eventIndex++;
        if(eventIndex>events.Count-1){
            Destroy(gameObject);
            return;
        }
        ParseFlags(CurrentEvent().initFlags);
    }
    void ParseFlags(List<BulletEvent.bulletEventFlags> flags){
        for(int i = 0; i < flags.Count; i++){
            switch(flags[i]){
                case BulletEvent.bulletEventFlags.delete:
                    Destroy(gameObject);
                    break;
            }
        }
    }
    public void SetEvents(List<BulletEvent> newEvents)
    {
        events = newEvents;
    }
    */
}
/*
[System.Serializable]
public class BulletEvent {

    public bool ChangeSpeed;
    public bool ChangeRotation;
    public float transitionTime;
    public Vector3 EulerRotation;
    public bool rotationIsRelatativeToLastRotation;//otherwise it is relative to starting rotation
    public bool speedChangeIsRelativeToLastSpeed;//otherwise is absolute
    public float speed;
    public enum bulletEventFlags{
        delete //delete is called automatically if no other events are listed
    }
    public List<bulletEventFlags> initFlags = new List<bulletEventFlags>();
    public List<bulletEventFlags> endFlags = new List<bulletEventFlags>();
    //flags trigger 
}
*/



[System.Serializable]
public class Bullet
{
    [SerializeField] bulletTypes type;

    public enum bulletTypes
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    public Bullet(bulletTypes b)
    {
        type = b;
    }
    public bulletTypes GetBulletType() => type;
    public Color GetColor()
    {
        switch (type)
        {
            case bulletTypes.Red:
                return Color.red;
            case bulletTypes.Green:
                return Color.green;
            case bulletTypes.Blue:
                return Color.blue;
            case bulletTypes.Yellow:
                return Color.yellow;
            default: return Color.black;
        }
    }

}
