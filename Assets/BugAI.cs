using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BugAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] BulletSpawner spawner;
    [SerializeField] bool stopMovingWhenFiring;
    [SerializeField] bool stopRotatingWhenFiring;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] float upcloseRotationSpd;
    // Update is called once per frame
    void Update()
    {
        hitParticles.transform.LookAt(player.transform, Vector3.up);
        if (spawner.isFiring())
        {
            //move
            if (stopMovingWhenFiring)
            {
                agent.destination = transform.position;
            }
            else
            {
                agent.SetDestination(player.transform.position);
            }
            //rotate
            if (agent.remainingDistance <= agent.stoppingDistance && !stopRotatingWhenFiring)
            {
                RotateTowards(player.transform);
            }
        }
        else
        {
            agent.SetDestination(player.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                RotateTowards(player.transform);
                spawner.Fire();
            }
        }
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0f;
        lookRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * upcloseRotationSpd);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            hitParticles.transform.LookAt(other.transform, Vector3.up);
            hitParticles.Play();
        }
    }
}
