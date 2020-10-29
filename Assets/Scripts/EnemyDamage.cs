using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{

    public int enemyHitPoints = 30;
    [SerializeField] ParticleSystem enemyHit = null;
    [SerializeField] ParticleSystem enemyDeath = null;
    Text scoreText;
    GameObject deathFXParent;
    ParticleSystem deathFX;
    WaveController waveController;
    PlayerHealth playerHealth;

    void Start()
    {   
        waveController = GameObject.FindWithTag("World").GetComponent<WaveController>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessDamage();
        if(enemyHitPoints <= 0)
        {
            DestroyEnemy(this.gameObject, enemyDeath);
        }
    }

    public void DestroyEnemy(GameObject enemy, ParticleSystem explosion)
    {
        // Get correct explosion pos and adj height
        Vector3 enemyDeathPos = new Vector3(
            enemy.transform.position.x,
            enemy.transform.position.y * 2f,
            enemy.transform.position.z
        );

        // Play correct explosion when destroying enemy
        deathFX = Instantiate(explosion, enemyDeathPos, Quaternion.identity);

        // Place death instance in parent and play death effect
        deathFXParent = GameObject.Find("Death Effects"); 
        deathFX.transform.parent = deathFXParent.transform;
        deathFX.Play();

        // Check for active enemies after each enemy destroyed
        waveController.Invoke("CheckActiveEnemies", 1f);
        Destroy(enemy);
    }

    private void ProcessDamage()
    {
        playerHealth.score += 1;
        if(playerHealth.score >= 999999) { playerHealth.score = 999999; } // Set max score 
        scoreText.text = "Score\n" + playerHealth.score.ToString();
        enemyHitPoints -= 2;
        enemyHit.Play();
    }

}