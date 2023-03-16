using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float autotargetRotation = 0f;
    [SerializeField] float speed = 2f;
    [SerializeField] float lifetime = 4f;
    [SerializeField] Rigidbody rb;
    [SerializeField] float damage = 1f;

    public float GetDamage => damage;
    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.velocity += transform.forward * speed;
    }
    void Update(){
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
