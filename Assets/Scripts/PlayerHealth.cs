using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int score = 0;
    [SerializeField] int health = 10;
    [SerializeField] int damage = 1;
    WaveController waveController;
    Text baseHealthText;

    void Start()
    {
        waveController = GameObject.FindWithTag("World").GetComponent<WaveController>();
        baseHealthText = GameObject.FindWithTag("BaseHealth").GetComponent<Text>();
        baseHealthText.text = "HP." + health.ToString();
    }

    public void DecreasePlayerHealth()
    {
        health -= damage;
        baseHealthText.text = "HP." + health.ToString();
        // Check if health reached zero
        if(health <= 0)
        {
            waveController.gameOver = true;
            waveController.Invoke("GameOver", 1f);
        }
    }
    
}
