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

    /*public override void EnemyBulletHitUpdate()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.2f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z * 1.2f);
        damage += 2;
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            Debug.Log("EatMyAss");
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.2f, gameObject.transform.localScale.y * 1.2f, gameObject.transform.localScale.z * 1.2f);
            damage += 2;
            Destroy(other.gameObject);
        }
        else if(other.tag == "PlayerBullet")
        {
            damage += 2;
            Destroy(other.gameObject);
        }
    }*/
}
