using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    private HealthEnemy healthEnemy;
    private EnemiesCounter enemiesCounter;
    public Transform playerTransform;
    private Animator animator;

    private PlayerMove playerMove;

    float timer = 0f;
    public float maxTime = 1f;

    public float attackDistance = 0.8f;

    public List<Transform> WP;
    private Vector3 waypoint;
    private Vector3 lastwaypoint;

    public string currentDestination;
    public float maxDistance;//= 1f;

    //Algoritmo Gen�tico
    public float attackDamage;
    public float duracion=0f;
    public int health;
    public float chaseDistance;// = 10f;
    public float doneDamage=0f;

    public EnemyAttributes attributes;
    public int maxHealth;
    public bool wasHit = false;
    /**********************************/

    private void Awake() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        waypoint = FindClosestTransform();
    }

    private void Start()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
        healthEnemy = GetComponent<HealthEnemy>();
        enemiesCounter = GameObject.FindWithTag("EnemiesCounter").GetComponent<EnemiesCounter>();
        enemiesCounter.IncrementEnemies();
        currentDestination = "waypoint";
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
       
    }

    private void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer < 0.0f && healthEnemy.IsAlive()) {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer > chaseDistance) {
                MoveBetweeenWaypoints();
            } else if (distanceToPlayer > attackDistance) {
                ChasePlayer();
            } else {
                AttackPlayer();
            }
            timer = maxTime;
           
        }
        if (healthEnemy.IsAlive() && wasHit)
        {
            duracion += 1f;
        }
        
        animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
    }



    private void MoveBetweeenWaypoints()
    {
        animator.SetBool("Attacking", false);
        navMeshAgent.speed = 1.34f;

        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 0.1f)
        {
            if (currentDestination.Equals("Player"))
            {
                navMeshAgent.destination = lastwaypoint;
                currentDestination = "waypoint";
            } else {
                int waypointIndex = 0;
                for (int i = 0; i < WP.Count; i++)
                {
                    if (WP[i].position == waypoint)
                    {
                        waypointIndex = i;
                        break;
                    }
                }

                int nextWaypointIndex = (waypointIndex + 1) % WP.Count;
            
                Vector3 nextWaypointPosition = WP[nextWaypointIndex].position;

                waypoint = nextWaypointPosition;
                navMeshAgent.destination = waypoint;
            }
        }
    }

    //CHASING PLAYER
    private void ChasePlayer() {
        navMeshAgent.speed = 4f;
        animator.SetBool("Attacking", false);
        float sqrDistance = (playerTransform.position - navMeshAgent.destination).sqrMagnitude;
        if (sqrDistance > maxDistance * maxDistance) {
            navMeshAgent.destination = playerTransform.position;
            currentDestination = "Player";
        }
        
    }

    //ATTACKING PLAYER
    private void AttackPlayer() {
        if (!playerMove.hasDie) {
            navMeshAgent.destination = transform.position;
            animator.SetBool("Attacking", true);
            
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            float attackRange = 2f;
    
            if (distanceToPlayer <= attackRange) {
                transform.LookAt(playerMove.GetComponent<Transform>());
                float typeOfAttack = Random.Range(0f, 2f);
                animator.SetFloat("typeOfAttack", typeOfAttack);
                
                HurtPlayer((int)typeOfAttack);
            }
        } else {
            animator.SetBool("Attacking", false);
        }
    }

    private void HurtPlayer(int typeOfAttack) {
        float damageAmount = 0f;
       // float minDamage = 10f;
        //float maxDamage = 20f;
        switch (typeOfAttack) {
            case 0:
                damageAmount = attackDamage;//Random.Range(minDamage, maxDamage);
                break;
            case 1:
                damageAmount = attackDamage*2;//Random.Range(minDamage * 2, maxDamage * 2);
                break;
            case 2:
                damageAmount = attackDamage/2;//Random.Range(minDamage / 2, maxDamage / 2);
                break;
        }
        playerMove.TakeDamage(damageAmount);
        doneDamage += damageAmount;
    }


    private Vector3 FindClosestTransform()
    {
        Transform closestTransform = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform transformToCompare in WP)
        {
            float distance = Vector3.Distance(transform.position, transformToCompare.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestTransform = transformToCompare;
               
            }
        }
        
        return new Vector3(closestTransform.position.x, closestTransform.position.y, closestTransform.position.z);
    }

    public void setAttributes()
    {
        attributes = new EnemyAttributes(attackDamage, duracion, health, chaseDistance, doneDamage);
    }
   
    public void AlgoritmoGenetico()
    {
        playerMove.GetComponent<EnemiesSpawn>().Enemigo(gameObject);
    }


}
