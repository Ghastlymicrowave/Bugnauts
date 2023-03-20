using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletTriColor : PlayerBullet
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

    private void OnTriggerEnter(Collider collision)
    {
        EnemyProjectile eP = collision.gameObject.GetComponent<EnemyProjectile>();
        Debug.Log(eP);
        if (eP != null)
        {
            Debug.Log("Blasted");
            Destroy(eP.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
