using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]Bullet storedBullet;
    public Bullet Bullet => storedBullet;
    public float lifetime;
    //[SerializeField]List<BulletEvent> events;
    //[SerializeField]int eventIndex = 0;
    //[SerializeField]float eventTimer = 0f;

    //float speed_last;
    //Quaternion rotation_last;
    //Quaternion initalRotation;
    [SerializeField]Rigidbody rb;
    Vector3 ang;
    float spd;

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
    */
    void Start()
    {
        //initalRotation = transform.rotation;
        //rotation_last = transform.rotation;
        //speed_last = currentSpeed;
        //GetComponent<Renderer>().material.color = storedBullet.GetColor();  
        //rb = GetComponent<Rigidbody>();
    }

    public void SetStart(float nspd, Vector3 angVel){
        ang = angVel;
        spd = nspd;
    }

    void FixedUpdate(){
        transform.Rotate(ang);
        rb.velocity = transform.forward * spd;
    }

    void Update(){
        lifetime-= Time.deltaTime;
        if(lifetime<=0){
            Destroy(gameObject);
        }
    }
    /*
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
