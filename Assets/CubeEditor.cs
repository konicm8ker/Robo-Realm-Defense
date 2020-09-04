using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // Runs scripts live in editor + playmode
[SelectionBase] // Prevents initial child selection
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x,
            0f,
            waypoint.GetGridPos().y
        ); // Auto snap to grid
    }

    private void UpdateLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>(); // Update labels with snapped coord
        int gridSize = waypoint.GetGridSize();
        string labelText =
            (waypoint.GetGridPos().x / gridSize) +
            "," +
            (waypoint.GetGridPos().y / gridSize);
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
