using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // Runs scripts live in editor + playmode
[SelectionBase] // Prevents initial child selection
public class CubeEditor : MonoBehaviour
{
    [SerializeField][Range(1f,20f)] float gridSize = 10f;
    TextMesh textMesh;

    void Update()
    {

        Vector3 snapPos;
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = new Vector3(snapPos.x, 0f, snapPos.z); // Auto snap to grid

        textMesh = GetComponentInChildren<TextMesh>(); // Update labels with snapped coord
        textMesh.text = (snapPos.x / gridSize) + "," + (snapPos.z / gridSize);
    }
}
