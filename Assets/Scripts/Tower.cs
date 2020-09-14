using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform enemyTarget;

    void Update()
    {
        objectToPan.LookAt(enemyTarget);
    }
}
