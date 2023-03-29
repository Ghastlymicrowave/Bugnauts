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
}
