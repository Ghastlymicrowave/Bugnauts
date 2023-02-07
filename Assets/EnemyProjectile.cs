using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]List<BulletEvent> events;
    [SerializeField]int eventIndex = 0;
    [SerializeField]float eventTimer = 0f;

    float speed_last;
    Quaternion rotation_last;
    Quaternion initalRotation;

    [SerializeField]float currentSpeed;

    void Start()
    {
        initalRotation = transform.rotation;
        rotation_last = transform.rotation;
        speed_last = currentSpeed;
    }
    BulletEvent CurrentEvent(){
        if(eventIndex>events.Count-1){
            Debug.Log("oops");
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
}
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
