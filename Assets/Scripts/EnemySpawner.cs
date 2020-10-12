using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Tooltip("In seconds")][Range(0.1f,120f)] float spawnTimer = 2f;
    [SerializeField] int spawnLimit = 10;
    [SerializeField] Transform enemyParent = null;
    [SerializeField] EnemyMovement enemyPrefab = null;
    PlayerHealth playerHealth = null;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Delay initial enemy spawn
        yield return new WaitForSeconds(spawnTimer);
        for(int i=0; i<spawnLimit; i++)
        {
            bool gameOverStatus = playerHealth.gameOver;
            if(gameOverStatus == true) { yield break; }

            EnemyMovement enemy = Instantiate(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);
            enemy.transform.parent = enemyParent;
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
