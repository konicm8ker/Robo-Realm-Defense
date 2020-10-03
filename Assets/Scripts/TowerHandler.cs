using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab = null;
    [SerializeField] Queue<Tower> towerQueue = new Queue<Tower>();
    [SerializeField] Transform towerParent = null;

    public void AddTower(Waypoint baseWaypoint)
    {
        if(towerQueue.Count < towerLimit)
        {
            InstantiateTower(baseWaypoint);
        }
        else
        {
            MoveTower(baseWaypoint);
        }
    }

    private void InstantiateTower(Waypoint baseWaypoint)
    {
        Tower newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);

        // Place instantiated tower in parent, set waypoint and flags
        newTower.transform.parent = towerParent;
        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        towerQueue.Enqueue(newTower);
    }

    private void MoveTower(Waypoint newBaseWaypoint)
    {
        Tower lastTower = towerQueue.Dequeue();

        // Set flags and waypoint position, then move tower
        lastTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;
        lastTower.baseWaypoint = newBaseWaypoint;
        lastTower.transform.position = newBaseWaypoint.transform.position;

        towerQueue.Enqueue(lastTower);
    }
}
