using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{

    public bool gameOver = false;
    public bool waveIsActive = false;
    [SerializeField] Text timerText = null;
    [SerializeField] Text waveText = null;
    [SerializeField] Text towerCount = null;
    [SerializeField] Text gameOverText = null;
    TowerHandler towerHandler;
    GameObject[] towers;
    int timerCounter = 3;
    int waveCount = 1;
    bool activeEnemies = true;
    

    void Start()
    {
        // Start initial wave
        waveText.text = "Wave " + waveCount;
        Invoke("StartFirstWave", 1f);
    }

    private void StartFirstWave()
    {
        // set initial values for wave and towercount
        waveText.text = "Wave " + waveCount;
        towerHandler = GameObject.FindObjectOfType<TowerHandler>();
        towerCount.text = "x" + towerHandler.towerLimit;

        // start countdown timer
        StartCoroutine(CountdownTimer());
    }

    private void StartNextWave()
    {
        // Reset timer counter for next countdown and update wave, reset towers
        timerCounter = 3;
        UpdateWaveStatus();
        IncreaseDifficulty();
        ResetTowers();
        // Start countdown timer
        StartCoroutine(CountdownTimer());
    }

    private void UpdateWaveStatus()
    {
        waveCount++;
        waveText.text = "Wave " + waveCount;
    }

    private void IncreaseDifficulty()
    {
        EnemySpawner enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();

        if(enemySpawner.spawnRate > 1f) // Set max spawn rate
        {
            enemySpawner.spawnRate -= .5f;
        }
        if(enemySpawner.spawnLimit < 10) // Set max spawn limit
        {
            enemySpawner.spawnLimit += 1;
        }
    }

    private void ResetTowers()
    {
        towerHandler = GameObject.FindObjectOfType<TowerHandler>();
        towers = GameObject.FindGameObjectsWithTag("Tower");

        foreach (GameObject tower in towers)
        {
            Tower towerInstance = tower.GetComponent<Tower>();
            Waypoint waypointToReset = towerInstance.baseWaypoint;
            waypointToReset.isPlaceable = true;
            Destroy(tower);
        }

        towerHandler.towerQueue.Clear();
        towerHandler.ResetTowerStats();
    }

    private void CheckActiveEnemies()
    {
        var enemyCount = GameObject.FindObjectsOfType<EnemyMovement>().Length;
        if(enemyCount == 0 && gameOver == false) // if no active enemies and no gameover
        {
            if(activeEnemies) // Only allow next wave to run once if no active enemies
            {
                waveIsActive = false;
                Invoke("StartNextWave", 1f);
            }
            activeEnemies = false;
        }
    }

    private void ProcessEnemySpawner()
    {
        waveIsActive = true;
        activeEnemies = true;
        var enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        StartCoroutine(enemySpawner.SpawnEnemies());
    }

    private void GameOver()
    {
        // Display GAME OVER text
        gameOverText.enabled = true;
    }

    IEnumerator CountdownTimer()
    {
        while(timerCounter >= 0)
        {
            string textToDisplay = timerCounter.ToString() + "..";
            if(timerCounter == 0) { textToDisplay = "DEFEND!"; }
            timerText.text = textToDisplay;
            timerCounter--;
            yield return new WaitForSeconds(1.2f);
        }
        
        timerText.text = "";
        ProcessEnemySpawner();
    }

}
