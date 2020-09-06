using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null;
    [SerializeField] Waypoint endWaypoint = null;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();

            // Check for duplicate blocks
            if(grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Duplicate block detected, skipping " + waypoint);
            }
            else
            {
                // Add block position and gameobject to dictionary
                grid.Add(gridPos, waypoint);
            }
        }
        // print("Loaded " + grid.Count + " blocks to dictionary.");
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }
}
