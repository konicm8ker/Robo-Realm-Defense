using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Public var okay here as they are data classes
    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom = null;
    [SerializeField] Tower towerPrefab = null;
    GameObject cursor;
    GameObject towers;
    Vector2Int gridPos;
    const int gridSize = 10;

    void OnMouseOver()
    {
        if(isPlaceable)
        {
            UpdateCursor(true);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable)
            {
                PlaceTower();
            }
        }
    }

    void OnMouseExit()
    {
        UpdateCursor(false);
    }

    private void UpdateCursor(bool render)
    {
        cursor = GameObject.Find("Cursor");
        MeshRenderer cursorMesh = cursor.GetComponent<MeshRenderer>();

        cursorMesh.enabled = render;
        cursor.transform.position = new Vector3(
            transform.position.x,
            cursor.transform.position.y,
            transform.position.z
        );
    }

    private void PlaceTower()
    {
        towers = GameObject.Find("Towers");
        Tower tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        tower.transform.parent = towers.transform; // Place all instantiated towers in parent
        isPlaceable = false;
        UpdateCursor(false);
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

}
