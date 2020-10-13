using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Public var okay here as they are data classes
    public bool isExplored = false;
    public bool isPlaceable = true;
    public Waypoint exploredFrom = null;
    PlayerHealth playerHealth = null;
    GameObject cursor;
    Vector2Int gridPos;
    byte r,g,b,a;
    const int gridSize = 10;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if(playerHealth.gameOver == true)
        {
            UpdateCursor(false, "null");
        }
    }

    void OnMouseOver()
    {
        if(isPlaceable)
        {
            UpdateCursor(true, "orange");
        }
        else
        {
            UpdateCursor(true, "red");
        }

        if(Input.GetMouseButtonDown(0))
        {
            var gameOverStatus = playerHealth.gameOver;
            if(isPlaceable && gameOverStatus == false)
            {
                FindObjectOfType<TowerHandler>().AddTower(this);
            }
            else
            {
                print("Can't place tower here.");
            }
        }
    }

    void OnMouseExit()
    {
        UpdateCursor(false, "null");
    }

    private void UpdateCursor(bool render, string color)
    {
        cursor = GameObject.Find("Cursor");
        MeshRenderer cursorMesh = cursor.GetComponent<MeshRenderer>();

        cursorMesh.enabled = render;
        if(color == "orange")
        { r=229; g=124; b=25; a=208; }
        if(color == "red")
        { r=229; g=32; b=25; a=208; }
        cursorMesh.material.SetColor("_Color", new Color32(r,g,b,a));
        cursor.transform.position = new Vector3(
            transform.position.x,
            cursor.transform.position.y,
            transform.position.z
        );
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
