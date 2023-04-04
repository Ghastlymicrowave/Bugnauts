using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float autotargetRotation = 0f;
    [SerializeField] float speed = 2f;
    [SerializeField] float lifetime = 4f;
    [SerializeField] Rigidbody rb;
    [SerializeField] protected float damage = 1f;

    [SerializeField] Color popColor;
    [SerializeField] GameObject popEffect;

    public float GetDamage => damage;
    public void SetDamage(float mul) { damage *= mul; }
    public void SetSpeed(float mul) { speed *= mul; }

    protected virtual void Start(){
        rb = GetComponent<Rigidbody>();
        rb.velocity += transform.forward * speed;
    }
    protected virtual void Update(){
        lifetime -= Time.deltaTime;
        if (lifetime<=0){
            Destroy(gameObject);
        }

        if (PauseManager.IsPaused)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag=="EnemyBullet" || collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "Bullet") { return; }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(popEffect, gameObject.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider collision)
    {
       /* if (collision.gameObject.tag != "Bullet")
            return;*/

        
            Debug.Log("hit bullet");

        //Destroy(collision.gameObject);

    }
}
