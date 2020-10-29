﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 2f;
    public int spawnLimit = 10;
    [SerializeField] Transform enemyParent = null;
    [SerializeField] EnemyDamage enemyPrefab = null;
    WaveController waveController;
    Text scoreText;
    int enemyHitCounter = 30; // Start with value of enemy prefab

    void Start()
    {
        waveController = GameObject.FindObjectOfType<WaveController>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        scoreText.text = "Score\n0";
    }

    private void IncreaseHitPoints(EnemyDamage enemy)
    {
        // Increase enemy hit points
        enemy.enemyHitPoints = enemyHitCounter;

    }

    public IEnumerator SpawnEnemies()
    {
        for(int i=0; i<spawnLimit; i++)
        {
            var gameOverStatus = waveController.gameOver;
            if(gameOverStatus == false)
            {
                EnemyDamage enemy = Instantiate(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);
                enemy.transform.parent = enemyParent;
                IncreaseHitPoints(enemy);
            }
            yield return new WaitForSeconds(spawnRate);
        }
        if(enemyHitCounter < 40) // Set max hit points for enemy
        {
            enemyHitCounter += 2;
        }
    }

}
