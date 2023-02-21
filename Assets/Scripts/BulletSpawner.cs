using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    //[SerializeField] List<BulletEvent> events;
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

    private void Start()
    {
        t = -initalDelay;
    }

    private void Update()
    {
        t += Time.deltaTime;
        if ( t > timeBetweenBursts)
        {
            float l = t - timeBetweenBursts;
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
    }

    void Restart()
    {
        bulletsFired = 0;
        t = 0f;
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
