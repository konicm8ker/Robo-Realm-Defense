using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 60;
    [SerializeField] ParticleSystem enemyHit = null;
    [SerializeField] ParticleSystem enemyDeath = null;
    GameObject deathFXParent = null;

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
        enemyHit.Play();
    }

    private void DestroyEnemy()
    {
        // Adjusted death particle explosion height
        Vector3 enemyDeathPos = new Vector3(transform.position.x, transform.position.y * 2f, transform.position.z);
        ParticleSystem enemyDeathFX = Instantiate(enemyDeath, enemyDeathPos, Quaternion.identity);

        // Place death instance in parent and play death effect
        deathFXParent = GameObject.Find("Death Effects"); 
        enemyDeathFX.transform.parent = deathFXParent.transform;
        enemyDeathFX.Play();

        Destroy(gameObject);
    }
}