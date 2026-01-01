using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;
    public Animator ani;

    public ParticleSystem partSystem;
    private AudioSource audioSource;

    //Enemies Counter
    public EnemiesCounter enemiesCounter;

    
    public Slider BarraVida;
    //Patrullar
    public bool walkPointSet;
    public float walkPointRange;
    public Vector3 waypoint;
    public Vector3 Lastwaypoint;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public GameObject proyectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Transform spawnPoint;
    
    public bool isMelee;
    private bool death=false;
    public List<Transform> WP;
    public string objetivo;

    //Algoritmo Genético
    public float velocidadAtaque;
    public float dañoAtaque;
    public float duración;
    public int health;
    /**********************************/
    private void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("SciFiGunSound").GetComponent<AudioSource>();    
        player = GameObject.FindWithTag("Player").transform;
        enemiesCounter = GameObject.FindWithTag("EnemiesCounter").GetComponent<EnemiesCounter>();
        agent = GetComponent<NavMeshAgent>();
        waypoint = FindClosestTransform();
        objetivo = "waypoint";
    }

    private void Start() {
        enemiesCounter.IncrementEnemies();
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
    void Update()
    {
  
        BarraVida.value = health;

        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!death)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        
    }

    private void Patroling()
    {
      
            ani.SetBool("walk", true);
            ani.SetBool("run", false);
        

        if (objetivo.Equals("Player"))
        {
            agent.SetDestination(Lastwaypoint);
            objetivo = "waypoint";
            walkPointSet = true;
        }
        ani.SetBool("attack", false);
        if (!walkPointSet)
        {
            
            agent.SetDestination(waypoint);
            walkPointSet = true;
        }


    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        ani.SetBool("walk", false); 
        ani.SetBool("run", true); 
        ani.SetBool("attack", false);
        objetivo = "Player";
    }

    private void AttackPlayer()
    {
        ani.SetBool("walk", false); 
        ani.SetBool("run", false); 
        ani.SetBool("attack", true);
        GetComponent<Rigidbody>().freezeRotation = true;
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            if (!isMelee)
            {
                partSystem.Play();
                audioSource.Play();
                GameObject bullet = Instantiate(proyectile, spawnPoint.position, spawnPoint.rotation);
                bullet.transform.rotation = Quaternion.Euler(0, 15, 90);
                bullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * 5000);              
                Invoke(nameof(resetAttack), timeBetweenAttacks);
            }

            alreadyAttacked = true;
        }
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " ha recibido " + damage);
        health -= damage;
        if (health <= 0) Invoke(nameof(destroyEnemy), 0.5f);
    }

    private void destroyEnemy()
    {
        enemiesCounter.DecrementEnemies();
        death = true;
        ani.SetTrigger("death");
        Invoke("DestroyEnemyDelayed", 1f);
    }

    private void DestroyEnemyDelayed()
    {
        // Destruir el enemigo
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    public void fin_Ani()
    {
        ani.SetBool("attack", false);
        alreadyAttacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("waypoint"))
        {
            Transform aux = other.GetComponent<WayPoint>().nextPoint;          
            waypoint = new Vector3(aux.position.x,aux.position.y,aux.position.z);
            walkPointSet = false;
            Lastwaypoint = waypoint;
        }
    }
}
