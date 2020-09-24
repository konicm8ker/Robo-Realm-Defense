using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 100;
    public bool enemyAlive = true;

    private void OnParticleCollision(GameObject other)
    {
        if(enemyHitPoints > 0)
        {
            enemyHitPoints -= 2;
            print("Enemy HP: " + enemyHitPoints);
            if(enemyHitPoints <= 0) { enemyAlive = false; }
        }
        if(!enemyAlive)
        {
            print("Enemy is dead.");
            this.gameObject.transform.localScale = new Vector3(0,0,0);
        }
    }

}