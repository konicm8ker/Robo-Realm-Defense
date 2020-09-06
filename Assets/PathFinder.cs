using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null;
    [SerializeField] Waypoint endWaypoint = null;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    }; 

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbors();
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

    private void ExploreNeighbors()
    {
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = startWaypoint.GetGridPos() + direction;
            // print("Exploring " + neighborCoordinates);
            try
            {
                grid[neighborCoordinates].SetTopColor(Color.blue);
            }
            catch
            {
                Debug.LogWarning("Block " + neighborCoordinates + " not in dictionary, skipping.");
            }
        }
    }
}
