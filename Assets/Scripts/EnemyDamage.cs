using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 60;
    [SerializeField] ParticleSystem enemyHit = null;
    [SerializeField] ParticleSystem enemyDeath = null;
    GameObject deathFXParent = null;
    ParticleSystem deathFX = null;

    private void OnParticleCollision(GameObject other)
    {
        ProcessDamage();
        if(enemyHitPoints <= 0)
        {
            DestroyEnemy(this.gameObject, enemyDeath);
        }
    }

    private void ProcessDamage()
    {
        enemyHitPoints -= 2;
        enemyHit.Play();
    }

    public void DestroyEnemy(GameObject enemy, ParticleSystem explosion)
    {
        // Get correct explosion pos and adj height
        Vector3 enemyDeathPos = new Vector3(
            enemy.transform.position.x,
            enemy.transform.position.y * 2f,
            enemy.transform.position.z
        );

        // Play correct explosion when destroying enemy
        deathFX = Instantiate(explosion, enemyDeathPos, Quaternion.identity);

        // Place death instance in parent and play death effect
        deathFXParent = GameObject.Find("Death Effects"); 
        deathFX.transform.parent = deathFXParent.transform;
        deathFX.Play();

        Destroy(enemy);
    }
}