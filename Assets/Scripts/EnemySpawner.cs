using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Tooltip("In seconds")][Range(0.1f,120f)] float spawnTimer = 2f;
    [SerializeField] Transform enemyParent = null;
    [SerializeField] EnemyMovement enemyPrefab = null;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Delay initial enemy spawn
        yield return new WaitForSeconds(spawnTimer);
        for(int i=0; i<5; i++)
        {
            EnemyMovement enemy = Instantiate(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);
            enemy.transform.parent = enemyParent;
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
