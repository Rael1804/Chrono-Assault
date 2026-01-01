using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public HealthEnemy healthEnemy;

    public void OnRaycastHit(GunController gunController, Vector3 direction) {
        healthEnemy.TakeDamage(gunController.damage, direction);
    }

}