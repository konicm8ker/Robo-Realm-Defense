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
        // Smooth enemy movement between waypoints
        for(int i=0; i<path.Count; i++)
        {
            Vector3 targetPos = path[i].transform.position;
            transform.LookAt(targetPos);
            while(Vector3.Distance(transform.position, targetPos) >= Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 3f);
                yield return null;
            }
            transform.LookAt(targetPos + new Vector3(0,0,1)); // Have enemy look up when on end waypoint
        }
        yield return null;
    }

}
