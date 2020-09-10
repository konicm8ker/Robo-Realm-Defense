using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Enemy starting patrol.");
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            // print("Visiting: " + waypoint.name);
            yield return new WaitForSeconds(1f);
        }
        print("Enemy ending patrol.");
    }
    
}
