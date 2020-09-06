using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    void Start()
    {
        LoadBlocks();
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
        print("Loaded " + grid.Count + " blocks to dictionary.");
    }
}
