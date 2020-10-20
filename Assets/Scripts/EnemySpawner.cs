using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Tooltip("In secs")][Range(0.1f,20f)] float spawnRate = 2f;
    [SerializeField] int spawnLimit = 10;
    [SerializeField] Transform enemyParent = null;
    [SerializeField] EnemyMovement enemyPrefab = null;
    WaveController waveController;
    Text scoreText;

    void Start()
    {
        waveController = GameObject.FindObjectOfType<WaveController>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        scoreText.text = "Score\n0";
    }

    public IEnumerator SpawnEnemies()
    {
        for(int i=0; i<spawnLimit; i++)
        {
            var gameOverStatus = waveController.gameOver;
            if(gameOverStatus == false)
            {
                EnemyMovement enemy = Instantiate(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);
                enemy.transform.parent = enemyParent;
            }
            yield return new WaitForSeconds(spawnRate);
        }
    }

}
