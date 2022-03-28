using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 25;
    public AudioClip deathSFX;
    public Slider healthSlider;
    public static int enemyCount = 0;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount++;
        currentHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount)
    {
        if(currentHealth != 0)
        {
            if (currentHealth > damageAmount)
            {
                currentHealth -= damageAmount;
            }
            else
            {
                currentHealth = 0;
                enemyCount--;
                AudioSource.PlayClipAtPoint(deathSFX, transform.position);
                if (enemyCount == 0)
                {
                    FindObjectOfType<LevelManager>().WinLevel();
                }
                Destroy(gameObject);
            }
        }

        healthSlider.value = currentHealth;
    }
}
