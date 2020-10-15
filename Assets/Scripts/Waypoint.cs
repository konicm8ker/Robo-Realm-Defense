using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    // Public var okay here as they are data classes
    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom = null;
    WaveController waveController = null;
    GameObject cursor;
    Vector2Int gridPos;
    const int gridSize = 10;

    void Start()
    {
        waveController = GameObject.FindObjectOfType<WaveController>();
    }

    void Update()
    {
        if(waveController.gameOver == true)
        {
            UpdateCursor(false);
        }
    }

    void OnMouseOver()
    {
        var gameOverStatus = waveController.gameOver;
        var waveStatus = waveController.waveIsActive;
        if(gameOverStatus == false && waveStatus == true)
        {
            UpdateCursor(true);
        }
        else
        {
            UpdateCursor(false);
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable && gameOverStatus == false && waveStatus == true)
            {
                FindObjectOfType<TowerHandler>().AddTower(this);
            }
            else
            {
                print("Can't place tower here.");
                // todo: flash cursor blank and play error sound
            }
        }
    }

    void OnMouseExit()
    {
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

}
