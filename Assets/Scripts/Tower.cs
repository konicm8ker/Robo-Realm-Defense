using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerToPan = null;
    [SerializeField][Range(1f,50f)] float attackRange = 30f;
    Transform enemyTarget;

    void Update()
    {
        SetTargetEnemy();
        if(enemyTarget)
        {
            ProcessAim();
            CheckEnemyDistance();
        }
        else
        {
            ProcessFiring(false);
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
    }
}
