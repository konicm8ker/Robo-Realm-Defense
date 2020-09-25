using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerToPan = null;
    [SerializeField] Transform enemyTarget = null;
    [SerializeField][Range(1f,50f)] float attackRange = 30f;

    void Update()
    {
        CheckEnemyExists();
    }

    private void CheckEnemyExists()
    {
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

    private void CheckEnemyDistance()
    {
        // Check if enemy is within range to shoot
        float enemyDistance = Vector3.Distance(enemyTarget.transform.position, gameObject.transform.position);
        if(enemyDistance < attackRange)
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
        Transform towerTop = this.gameObject.transform.Find("Tower_A_Top").transform.GetChild(0);
        ParticleSystem.EmissionModule emissionModule = towerTop.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = value;
    }
}
