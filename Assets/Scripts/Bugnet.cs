using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugnet : MonoBehaviour
{
    [SerializeField] PlayerBulletManager catcher;
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
            EnemyProjectile e = other.transform.parent.gameObject.GetComponent<EnemyProjectile>();

            if (t == 0f && e!=null){
                Debug.Log(e.gameObject.name);
                if (e.Bullet() != null)
                {
                    Bullet b = e.Bullet();
                    catcher.AddBullet(b);
                    t = collectCooldown;
                }
                
                
                
            }

            Destroy(other.transform.parent.gameObject);
            
        }
    }
}
