using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool gameOver = false;
    [SerializeField] int health = 10;
    [SerializeField] int damage = 1;

    void Start()
    {
        print("Player Health: " + health);
    }

    public void DecreasePlayerHealth()
    {
        health -= damage;
        print("Player Health: " + health);
        // Check if health reached zero
        if(health <= 0)
        {
            print("GAME OVER!");
            gameOver = true;
        }
    }
}
