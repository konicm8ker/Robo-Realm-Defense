using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab = null;
    GameObject towers = null;
    int towerCount = 0;

    public void AddTower(Waypoint waypoint)
    {
        if(towerCount < towerLimit)
        {
            InstantiateTower(waypoint);
        }
        else
        {
            MoveTower();
        }

    }

    private void InstantiateTower(Waypoint waypoint)
    {
        towers = GameObject.Find("Towers");
        Tower tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity);
        tower.transform.parent = towers.transform; // Place all instantiated towers in parent
        waypoint.isPlaceable = false;
        towerCount++;
    }

    private static void MoveTower()
    {
        print("Can't add more than 5 towers!");
        // Move towers using ring buffer
    }
}
