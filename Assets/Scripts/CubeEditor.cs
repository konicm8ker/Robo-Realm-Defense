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
            waypoint.GetGridPos().x * gridSize,
            0f,
            waypoint.GetGridPos().y * gridSize
        ); // Auto snap to grid
    }

    private void UpdateLabel()
    {
        // Update labels with snapped coord
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText =
            (waypoint.GetGridPos().x) +
            "," +
            (waypoint.GetGridPos().y);
        textMesh.text = labelText;
        gameObject.name = labelText;
    }

}
