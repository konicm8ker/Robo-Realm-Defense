using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 100;

    private void OnParticleCollision(GameObject other)
    {
        ProcessDamage();
        if(enemyHitPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    private void ProcessDamage()
    {
        enemyHitPoints -= 2;
        print("Enemy HP: " + enemyHitPoints);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}