using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public bool gameOver = false;
    [SerializeField] Text timerText = null;
    [SerializeField] Text waveText = null;
    [SerializeField] Text towerCount = null;
    [SerializeField] Text gameOverText = null;
    TowerHandler towerHandler;
    Tower tower;
    int timerCounter = 3;
    int waveCount = 1;
    GameObject[] towers;
    

    void Start()
    {
        // Start initial wave
        StartFirstWave();
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
        print("Starting next wave.");
        // set initial values for wave and towercount
        waveCount++;
        timerCounter = 3;
        waveText.text = "Wave " + waveCount;
        towerHandler = GameObject.FindObjectOfType<TowerHandler>();
        towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach(Tower tower in towerHandler.towerQueue)
        {
            var waypointToReset = tower.baseWaypoint;
            waypointToReset.isPlaceable = true;
        }
        towerHandler.towerQueue.Clear();
        foreach(GameObject tower in towers)
        {
            Destroy(tower);
        }
        towerHandler.ResetTowerStats();

        // start countdown timer
        StartCoroutine(CountdownTimer());
    }

    private void CheckActiveEnemies()
    {
        print("Checking for active enemies.");
        var enemyCount = GameObject.FindObjectsOfType<EnemyMovement>().Length;
        if(enemyCount <= 0 && gameOver == false) // if no active enemies and no gameover
        {
            Invoke("StartNextWave", 1f);
        }
    }

    public void GameOver()
    {
        print("GAME OVER!");
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

    private void ProcessEnemySpawner()
    {
        EnemySpawner enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        StartCoroutine(enemySpawner.SpawnEnemies());
    }
}
