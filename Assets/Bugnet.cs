using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bugnet : MonoBehaviour
{
    [SerializeField] BulletCatcherUI catcher;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            catcher.AddBullet(other.GetComponent<EnemyProjectile>().Bullet);
            Destroy(other.gameObject);
        }
    }
}
