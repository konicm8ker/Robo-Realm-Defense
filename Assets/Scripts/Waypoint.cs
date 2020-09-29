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
    GameObject towers;
    Vector2Int gridPos;
    const int gridSize = 10;

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable)
            {
                PlaceTower();
            }
        }
    }

    private void PlaceTower()
    {
        print("Placing tower on : " + gameObject.name);
        towers = GameObject.Find("Towers");
        Tower tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        tower.transform.parent = towers.transform; // Place all instantiated towers in parent
        isPlaceable = false;
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
