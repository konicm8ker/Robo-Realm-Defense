using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 12f;
    [SerializeField] ParticleSystem friendlyDeath = null;

    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        List<Waypoint> path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
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
        }
        yield return null;
        DamageBase();
    }

    private void DamageBase()
    {
        // Destroy enemy and play friendly explosion
        FindObjectOfType<EnemyDamage>().DestroyEnemy(this.gameObject, friendlyDeath);
    }
}
