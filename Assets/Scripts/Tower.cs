using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class Tower : MonoBehaviour
{

    public Waypoint baseWaypoint;
    [SerializeField] Transform towerToPan = null;
    [SerializeField][Range(1f,50f)] float attackRange = 30f;
    WaveController waveController;
    Transform enemyTarget;
    AudioSource audioSource;
    AudioClip towerFiringSFX;
    bool playFiringSFX = false;

    void Start()
    {
        audioSource = FindObjectOfType<TowerHandler>().GetComponent<AudioSource>();
        towerFiringSFX = FindObjectOfType<TowerHandler>().towerFiringSFX;
        waveController = GameObject.FindObjectOfType<WaveController>();
        StartCoroutine(FiringSFX(1.0f));
    }

    void Update()
    {
        SetTargetEnemy();
        bool gameOverStatus = waveController.gameOver;
        if(enemyTarget && gameOverStatus == false)
        {
            ProcessAim();
            CheckEnemyDistance();
        }
        else
        {
            ProcessFiring(false);
        }
    }

    IEnumerator FiringSFX(float waitTime)
    {
        while(true)
        {
            if(playFiringSFX)
            {
                // Play firing sfx
                audioSource.PlayOneShot(towerFiringSFX, 0.4f);
            }
            yield return new WaitForSeconds(waitTime);
        }
    } 

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform; // Assuming 1st enemy is closest
        foreach(EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, testEnemy.transform);
        }
        enemyTarget = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform transformA, Transform transformB)
    {
        // Get distance from assumed nearest (A) enemy and the enemy being tested (B)
        float distToA = Vector3.Distance(transform.position, transformA.position);
        float distToB = Vector3.Distance(transform.position, transformB.position);
        if(distToB < distToA)
        {
            return transformB;
        }
        return transformA;
    }

    private void CheckEnemyDistance()
    {
        // Check if enemy is within range to shoot
        float enemyDistance = Vector3.Distance(enemyTarget.transform.position, gameObject.transform.position);
        if(enemyDistance <= attackRange)
        {
            ProcessFiring(true);
        }
        else
        {
            ProcessFiring(false);
        }
    }

    private void ProcessAim()
    {
        towerToPan.LookAt(enemyTarget);
    }

    private void ProcessFiring(bool value)
    {
        Transform towerTop = gameObject.transform.Find("Tower_A_Top").GetChild(0);
        ParticleSystem.EmissionModule emissionModule = towerTop.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = value;
        playFiringSFX = value;
    }
  
}
