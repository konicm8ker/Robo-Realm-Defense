﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Public var okay here as they are data classes
    public bool isExplored = false;
    public Waypoint exploredFrom = null;
    [SerializeField] Color exploredColor = new Color(25f,69f,142f,0f);
    [SerializeField] Color defaultColor = new Color(255f,140f,0f,0f);
    Vector2Int gridPos;
    const int gridSize = 10;

    void Update()
    {
        if(isExplored)
        {
            SetTopColor(exploredColor);
        }
        else
        {
            SetTopColor(defaultColor);
        }
    }
    
    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    public void SetTopColor(Color color)
    {
        // Find "Top" mesh renderer and changes material color
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}