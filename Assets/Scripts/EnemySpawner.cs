using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Tooltip("In seconds")] float spawnTimer = 2f;
    [SerializeField] Transform parent;
    [SerializeField] GameObject enemy;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for(int i=0; i<5; i++)
        {
            print("Spawning enemy." + Time.time);
            GameObject enemySpawn = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            enemySpawn.transform.parent = parent;
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
