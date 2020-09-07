using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null;
    [SerializeField] Waypoint endWaypoint = null;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    [SerializeField] bool isRunning = true;

    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        PathSearch();
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

    private void PathSearch()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning)
        {
            Waypoint searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter); // TODO: remove log later
            HaltIfEndFound(searchCenter);
            ExploreNeighbors(searchCenter);
            searchCenter.isExplored = true;
        }

        print("Finished pathfinding?");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if(searchCenter == endWaypoint)
            {
                print("End waypoint found. Skipping path search.");
                isRunning = false;
            }
    }

    private void ExploreNeighbors(Waypoint from)
    {
        if(!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = from.GetGridPos() + direction;
            // print("Exploring " + neighborCoordinates);
            try
            {
                QueueNewNeighbors(neighborCoordinates);
            }
            catch
            {
                // print("Block " + neighborCoordinates + " not in dictionary, skipping.");
            }
        }
    }

    private void QueueNewNeighbors(Vector2Int neighborCoordinates)
    {
        Waypoint neighbor = grid[neighborCoordinates];
        if(neighbor.isExplored)
        {
            // Do nothing
        }
        else
        {
            neighbor.SetTopColor(Color.blue); // TODO: Move later
            queue.Enqueue(neighbor);
            print("Queuing " + neighbor);
        }
    }
}
