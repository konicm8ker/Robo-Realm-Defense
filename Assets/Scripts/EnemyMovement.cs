﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemySpeed = 12f;
    [SerializeField] ParticleSystem friendlyDeath = null;
    WaveController waveController;
    PlayerHealth playerHealth;
    EnemyDamage enemyDamage;

    void Start()
    {
        waveController = GameObject.FindWithTag("World").GetComponent<WaveController>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        enemyDamage = GameObject.FindWithTag("Enemy").GetComponent<EnemyDamage>();
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<Waypoint> path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    private void DamageBase()
    {
        // Decrease player health
        playerHealth.DecreasePlayerHealth();
        // Destroy enemy and play friendly explosion
        enemyDamage.DestroyEnemy(this.gameObject, friendlyDeath);
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
            bool gameOverStatus = waveController.gameOver;
            if(gameOverStatus == true) { yield break; }
        }
        yield return null;
        DamageBase();
    }
    
}
