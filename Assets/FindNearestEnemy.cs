using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestEnemy : MonoBehaviour
{
    [SerializeField] Camera cam;
    List<GameObject>enemies= new List<GameObject>();
    public GameObject closestEnemy;
    [SerializeField] RectTransform reticle;
    [SerializeField] float smoothTime;
    Smoothing s;
    Vector2 startPos = Vector2.zero;
    Vector2 screen = Vector2.zero;
    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag=="Enemy"){
            enemies.Add(col.gameObject);
            Debug.Log("enemy in range");
        }
    }
    void OnTriggerExit(Collider col){
        if(col.gameObject.tag=="Enemy"){
            if(enemies.Contains(col.gameObject)){
                enemies.Remove(col.gameObject);
            }
        }
    }

    Vector2 reticleTargetPos = Vector2.zero;

    void Update(){
        Vector2 center = new Vector2(Screen.width,Screen.height);
        int closestID = -1;
        float closestDist = -1f;
        for(int i=0; i < enemies.Count; i++){
            Vector2 pos = cam.WorldToScreenPoint(enemies[i].transform.position);
            float dist = Vector2.Distance(pos,center);
            if(closestID==-1 || dist < closestDist){
                closestID = i;
                closestDist = dist;
                continue;
            }
        }
        if(closestID!=-1){
            GameObject nclosestEnemy = enemies[closestID];
            CompareNewEnemy(nclosestEnemy);
        }else{
            GameObject nclosestEnemy= null;
            CompareNewEnemy(nclosestEnemy);
        }
        if(closestEnemy!=null){
            Vector2 v = cam.WorldToViewportPoint(closestEnemy.transform.position);
            v.x *= Screen.width;
            v.y *= Screen.height;
            reticleTargetPos = v;
        }

        if(s==null){
            reticle.position = reticleTargetPos;
        }else{
            float t = s.TickVal(Time.deltaTime);
            reticle.position = Vector2.Lerp(startPos,reticleTargetPos,t);
            if(t>=1f){
                s = null;
            }
        }
        ChangeViewSize();
    }

    void ChangeViewSize(){
        if (screen != new Vector2(Screen.width,Screen.height)){
            screen = new Vector2(Screen.width,Screen.height);
            if(closestEnemy==null){
                reticleTargetPos = new Vector2(Screen.width/2f,Screen.height/2f);
            }
        }
    }

    void CompareNewEnemy(GameObject newClosest){
        if(newClosest != closestEnemy){
            closestEnemy = newClosest;
            s = new Smoothing(0f,smoothTime,Smoothing.smoothingTypes.InFastOutSlow);
            if(closestEnemy==null){
                reticleTargetPos = new Vector2(Screen.width/2f,Screen.height/2f);
            }
            startPos = reticle.position;
        }
    }
}
