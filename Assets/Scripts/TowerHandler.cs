using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHandler : MonoBehaviour
{
    
    public Queue<Tower> towerQueue = new Queue<Tower>();
    public int towerLimit = 5;
    public AudioClip towerFiringSFX;
    [SerializeField] Tower towerPrefab = null;
    [SerializeField] Transform towerParent = null;
    [SerializeField] Text towerCount = null;
    [SerializeField] AudioClip towerPlacedClick;
    AudioSource audioSource;
    int towerLimitCounter;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResetTowerStats();
    }

    public void ResetTowerStats()
    {
        towerLimitCounter = towerLimit;
        towerCount.text = "x" + towerLimitCounter.ToString();
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        if(towerQueue.Count < towerLimit)
        {
            // Play sfx when placed
            audioSource.PlayOneShot(towerPlacedClick, 1.0f);
            InstantiateTower(baseWaypoint);
        }
        else
        {
            // Play sfx when placed
            audioSource.PlayOneShot(towerPlacedClick, 1.0f);
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
        towerLimitCounter--;
        towerCount.text = "x" + towerLimitCounter.ToString();
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
