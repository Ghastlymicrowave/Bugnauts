using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] List<BulletEvent> events;
    [SerializeField] float timeBetweenBursts;
    [SerializeField] float burstLength;
    [SerializeField] float bulletsPerBurst;
    [SerializeField] float initalDelay;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Vector3 WorldRotationStart;
    [SerializeField] Vector3 WorldRotationEnd;
    [SerializeField] Vector2 WorldPositionStart;
    [SerializeField] Vector2 WorldPositionEnd;
    float t = 0f;
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
            float timeBetween = burstLength / bulletsPerBurst;
            float n = Mathf.Floor(l / timeBetween);
            while (bulletsFired < n)
            {
                bulletsFired += 1;
                CreateBullet(bulletsFired);
            }
            if (n == bulletsPerBurst)
            {
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
        pro.SetEvents(events);
        obj.transform.position = 
    }
}
