using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public float maxLife;
    public float currentLife;
    private bool hasDie = false;
    private RagDollController ragDollController;
    public HealthBar healthBar;
    private EnemiesCounter enemiesCounter;

    public float force = 10f;

    private void Start()
    {
        maxLife = gameObject.GetComponent<EnemyController>().health;
        enemiesCounter = GameObject.FindWithTag("EnemiesCounter").GetComponent<EnemiesCounter>();
        currentLife = maxLife;
        ragDollController = GetComponent<RagDollController>();
        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody in rigidbodies) {
            HitBox hitBox = rigidbody.gameObject.AddComponent<HitBox>();
            hitBox.healthEnemy = this;
        }
    }

    public void TakeDamage(float damage, Vector3 direction) {
        currentLife -= damage;  
        healthBar.SetHealthBar(currentLife/maxLife);
        if (currentLife <= 0 && !hasDie) {
            hasDie = true;
            Die(direction);
        
        }
      
            gameObject.GetComponent<EnemyController>().wasHit = true;
        
        
    }

    private void Die(Vector3 direction) {
        ragDollController.RagDollOn();
        ragDollController.AddForce(direction * force);
        healthBar.TargetDied();
        enemiesCounter.DecrementEnemies();
        enemiesCounter.IncrementKills();

        //NUEVO//
        gameObject.GetComponent<EnemyController>().setAttributes();
        gameObject.GetComponent<EnemyController>().AlgoritmoGenetico();
    }

    public bool IsAlive() {
        return currentLife > 0;
    }
}
