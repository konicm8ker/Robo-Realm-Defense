using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 12f;
    [SerializeField] ParticleSystem friendlyDeath = null;
    PlayerHealth playerHealth = null;
    EnemyDamage enemyDamage = null;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        enemyDamage = GameObject.FindWithTag("Enemy").GetComponent<EnemyDamage>();
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<Waypoint> path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        // Smooth enemy movement between waypoints
        for(int i=0; i<path.Count; i++)
        {
            Vector3 targetPos = new Vector3(
                path[i].transform.position.x,
                transform.position.y,
                path[i].transform.position.z
            );
            transform.LookAt(targetPos);
            while(transform.position != targetPos)
            {   
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemySpeed);
                yield return null;
            }
            bool gameOverStatus = playerHealth.gameOver;
            if(gameOverStatus == true) { yield break; }
        }
        yield return null;
        DamageBase();
    }

    private void DamageBase()
    {
        // Decrease player health
        playerHealth.DecreasePlayerHealth();
        // Destroy enemy and play friendly explosion
        enemyDamage.DestroyEnemy(this.gameObject, friendlyDeath);
    }
}
