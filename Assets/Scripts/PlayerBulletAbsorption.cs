using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletAbsorption : PlayerBullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyProjectile eP = collision.gameObject.GetComponent<EnemyProjectile>();
        PlayerBullet pB = collision.gameObject.GetComponent<PlayerBullet>();
        if (eP != null)
        {
            damage += 2;
            Destroy(eP.gameObject);
        }
        else if(pB != null)
        {
            damage += 2;
            Destroy(pB.gameObject);
        }
    }
}
