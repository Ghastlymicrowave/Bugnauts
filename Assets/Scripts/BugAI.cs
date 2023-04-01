using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BugAI : MonoBehaviour
{
    [Header("AI properties")]
    
    [SerializeField] bool WanderInStationaryState;
    [SerializeField] bool ReturnToOriginInStationaryState;
    [SerializeField] bool stopMovingWhenFiring;
    [SerializeField] bool stopRotatingWhenFiring;
    [SerializeField] float upcloseRotationSpd;
    [SerializeField] float visionRange;
    [SerializeField] LayerMask playerMask;
    [SerializeField] float attackRange;
    [SerializeField] float stationaryWanderRadius;

    [Header("References")]
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] BulletSpawner spawner;


    [SerializeField] bool sawLastFrame;
    [SerializeField] bool playerSeen = false;
    [SerializeField] AIState state = AIState.STATIONARY;
    [SerializeField] float maxSearchingWanderTime;
    float searchWanderTime =0f;

    float agentSpd;
    float agentAnglSpd;
    public enum AIState
    {
        STATIONARY,
        CHASE,
        SEARCHING,
        SEARCHING_WANDER
    }

    Rigidbody rb;

    [SerializeField] Vector3 lastKnownPlayerPosition;
    [SerializeField] Vector3 lastKnownPlayerDirection;

    Vector3 startPos;

    // Update is called once per frame

    [SerializeField] float MaxHealth;
    [SerializeField] float healthAnimSmoothTime;
    float Health;

    [SerializeField] SpriteMask mask;

    Smoothing healthSmooth;
    float startPercent;
    float targetPercent;

    float PercentHealth()
    {
        return Health / MaxHealth;
    }
    void Start(){
        player = GameObject.Find("PlayerCenter");
        Health = MaxHealth;
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        agentSpd = agent.speed;
        agentAnglSpd = agent.angularSpeed;
    }

    private void FixedUpdate()
    {
        if (PauseManager.IsPaused)
        {
            return;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < visionRange)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized * visionRange, out hit, visionRange, playerMask);
            Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * visionRange, Color.red,2f);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "Player")
                {
                    if (sawLastFrame)
                    {
                        lastKnownPlayerDirection = (player.transform.position - lastKnownPlayerPosition).normalized;
                    }
                    lastKnownPlayerPosition = player.transform.position;
                    sawLastFrame = true;
                    playerSeen = true;
                }
                else
                {
                    sawLastFrame = false;
                    playerSeen = false;
                    Debug.Log(hit.transform.gameObject.name);
                }
            }
            else
            {
                playerSeen = false;
            }
        }
        else
        {
            playerSeen = false;
        }
    }

    void Update()
    {
        if (PauseManager.IsPaused)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            agent.speed = 0f;
            agent.angularSpeed = 0f;
            return;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            agent.speed = agentSpd;
            agent.angularSpeed = agentAnglSpd;
        }
        switch (state)
        {
            case AIState.STATIONARY:

                
                if (ReturnToOriginInStationaryState)
                {
                    if (Vector3.Distance(transform.position, startPos) > stationaryWanderRadius)
                    {
                        agent.SetDestination(startPos);
                    }
                    else if (Vector3.Distance(transform.position, agent.destination) < 1f && WanderInStationaryState)
                    {
                        float r = Random.Range(0f, Mathf.PI*2f);
                        float x = Mathf.Cos(r) * stationaryWanderRadius;
                        float z = Mathf.Sin(r) * stationaryWanderRadius;
                        agent.SetDestination(startPos + new Vector3(x, 0f, z));
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, agent.destination) < 1f && WanderInStationaryState)
                    {
                        float r = Random.Range(0f, Mathf.PI * 2f);
                        float x = Mathf.Cos(r) * stationaryWanderRadius;
                        float z = Mathf.Sin(r) * stationaryWanderRadius;
                        agent.SetDestination(startPos + new Vector3(x, 0f, z));
                    }
                }


                if (playerSeen)
                {
                    state = AIState.CHASE;
                }
                break;
            case AIState.CHASE:

                if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
                {
                    agent.SetDestination(transform.position);
                }
                else
                {
                    agent.SetDestination(player.transform.position);
                }
                

                if (agent.remainingDistance <= attackRange)
                {
                    if (spawner.isFiring())
                    {
                        if (!stopRotatingWhenFiring)
                        {
                            RotateTowards(player.transform);
                        }
                    }
                    else
                    {
                        spawner.Fire();
                        
                        RotateTowards(player.transform);
                    }
                    
                }

                if (!playerSeen)
                {
                    state = AIState.SEARCHING;
                }
                break;
            case AIState.SEARCHING:

                agent.SetDestination(lastKnownPlayerPosition);
                if (playerSeen)
                {
                    state = AIState.CHASE;
                }
                if (Vector3.Distance(transform.position, lastKnownPlayerPosition) < 2f)
                {
                    state = AIState.SEARCHING_WANDER;
                    searchWanderTime = maxSearchingWanderTime;
                    Vector3 v = lastKnownPlayerDirection;
                    v.y = 0f;
                    agent.SetDestination(lastKnownPlayerPosition + v * 1f);
                }
                break;
            case AIState.SEARCHING_WANDER:


                if (Vector3.Distance(transform.position, agent.destination) < 1f)
                {
                    Vector3 v = lastKnownPlayerDirection;
                    v.y = 0f;
                    agent.SetDestination(transform.position + v * 1f);
                }

                searchWanderTime -= Time.deltaTime;
                if (playerSeen)
                {
                    state = AIState.CHASE;
                }
                if (searchWanderTime <= 0)
                {
                    state = AIState.STATIONARY;
                }
                break;
        }

        hitParticles.transform.LookAt(player.transform, Vector3.up);
        if (spawner.isFiring())
        {
            //move
            if (stopMovingWhenFiring)
            {
                agent.destination = transform.position;
            }
            //rotate
            
        }
        

        if(healthSmooth != null)
        {
            float t = healthSmooth.TickVal(Time.deltaTime);
            mask.alphaCutoff = Mathf.Lerp(startPercent, targetPercent, t);
            if (t >= 1)
            {
                healthSmooth = null;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hitParticles.transform.LookAt(collision.transform, Vector3.up);
            hitParticles.Play();
            PlayerBullet b = collision.gameObject.GetComponent<PlayerBullet>();
            Destroy(b.gameObject);
            if (isActiveAndEnabled)
            {
                TakeDamage(b.GetDamage);
            }
            
        }
    }

    public void Kill()
    {
        GameTracker gt = GameObject.Find("Canvas").GetComponent<GameTracker>();
        if(gt != null)
        {
            gt.KillBug();
        }
        GameObject.Find("visionRange").GetComponent<FindNearestEnemy>().ClearActive(gameObject);
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        
        healthSmooth = new Smoothing(0f, healthAnimSmoothTime, Smoothing.smoothingTypes.InFastOutSlow);
        startPercent = PercentHealth();
        Health = Mathf.Max(0f, Health - damage);
        targetPercent = PercentHealth();

        //replace with an animation trigger:
        if (Health <= 0) { Kill(); }
    }

}
