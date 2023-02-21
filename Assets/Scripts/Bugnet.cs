using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugnet : MonoBehaviour
{
    [SerializeField] BulletCatcherUI catcher;
    // Start is called before the first frame update
    [SerializeField] float collectCooldown;
    float t = 0f;
    void Update(){
        if(t>0){
            t = Mathf.Max(0f,t-Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            if(t == 0f){
                catcher.AddBullet(other.GetComponent<EnemyProjectile>().Bullet);
                t = collectCooldown;
            }
            Destroy(other.gameObject);
        }
    }
}
