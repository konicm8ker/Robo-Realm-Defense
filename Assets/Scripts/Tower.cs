using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class Tower : MonoBehaviour
{
    Transform towerToPan = null;
    Transform enemyTarget = null;

    void Start()
    {
        towerToPan = GetComponent<Transform>().Find("Tower_A_Top"); // Only pan top of tower
        enemyTarget = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
    }

    void Update()
    {
        ProcessAim();
        ProcessFiring();
    }

    private void ProcessAim()
    {
        towerToPan.LookAt(enemyTarget);
    }

    private void ProcessFiring()
    {
        // instantiate particle firing
    }
}
