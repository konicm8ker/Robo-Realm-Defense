using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool gameOver = false;
    public int score = 0;
    [SerializeField] int health = 10;
    [SerializeField] int damage = 1;
    Text baseHealth;

    void Start()
    {
        baseHealth = GameObject.FindWithTag("BaseHealth").GetComponent<Text>();
        baseHealth.text = health.ToString() + ".HP";
    }

    public void DecreasePlayerHealth()
    {
        health -= damage;
        baseHealth.text = health.ToString() + ".HP";
        // Check if health reached zero
        if(health <= 0)
        {
            print("GAME OVER!");
            gameOver = true;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
