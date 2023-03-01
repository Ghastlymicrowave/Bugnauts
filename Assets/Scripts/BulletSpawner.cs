using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    //[SerializeField] List<BulletEvent> events;
    [SerializeField]bool autofire;
    [SerializeField][Range(0f,20f)] float overallMultiplier;
    [SerializeField] float timeBetweenBursts;
    [SerializeField] float burstLength;
    [SerializeField] int bulletsPerBurst;
    [SerializeField] float initalDelay;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Vector3 RotationStart;
    [SerializeField] Vector3 RotationEnd;
    [SerializeField] Vector3 PositionStart;
    [SerializeField] Vector3 PositionEnd;
    [SerializeField] Vector3 angularVelocityStart;
    [SerializeField] Vector3 angularVelocityEnd;
    [SerializeField] float lifetime;
    [SerializeField] float spdStart;
    [SerializeField] float spdEnd;
    [SerializeField]float t = 0f;
    [SerializeField][Range(0f,1f)] float offset;
    int bulletsFired;
    [SerializeField]bool fire = false;
    private void Start()
    {
        fireDelay = -initalDelay;
    }
    [SerializeField] bool firing = false;
    [SerializeField] float fireDelay;
    public void Fire()
    {
        fire = true;
    }
    public bool isFiring() { return t > 0f;}

    private void Update()
    {
        fireDelay += Time.deltaTime;
        if (fireDelay >= timeBetweenBursts) { fireDelay = timeBetweenBursts; }
        if (fireDelay >= timeBetweenBursts && (fire||autofire))
        {
            firing = true;
        }
        if (firing) {
            t += Time.deltaTime;
            float l = t;
            float timeBetween = burstLength / (float)bulletsPerBurst;
            if(timeBetween!=0){
                float n = Mathf.Floor(l / timeBetween);
                while (bulletsFired < n)
                {
                    CreateBullet(bulletsFired);bulletsFired ++;
                }
                if (n >= bulletsPerBurst)
                {
                    Restart();
                }
            }else{
                for(int i = 0; i < bulletsPerBurst;i++){
                    CreateBullet(bulletsFired);bulletsFired++;
                }
                Restart();
            }
            
        }
        fire = false;
    }

    void Restart()
    {
        bulletsFired = 0;
        t = 0f;
        fireDelay = 0f;
        firing = false;
    }

    void CreateBullet(int n)
    {
        GameObject obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        EnemyProjectile pro = obj.GetComponent<EnemyProjectile>();
        //Rigidbody rb = obj.GetComponent<Rigidbody>();
        //rb.AddTorque(angularVelocity,ForceMode.Acceleration);
        //rb.AddForce(velocity,ForceMode.Impulse);
        //pro.SetEvents(events);
        
        float t = (((float)n)+offset)/ (float)bulletsPerBurst;
        pro.SetStart(Mathf.Lerp(spdStart,spdEnd,t) * overallMultiplier,Vector3.Lerp(angularVelocityStart,angularVelocityEnd,t) * overallMultiplier);
        obj.transform.position = transform.position + Vector3.Lerp(PositionStart,PositionEnd,t);
        obj.transform.localRotation = Quaternion.Euler( Vector3.Lerp(RotationStart,RotationEnd,t));
        pro.lifetime = lifetime;
        //pro.SetStartingSpeed();
        //pro.SetStartingRot();
    }
}
