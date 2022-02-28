using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public AudioClip playerDeath;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0 && !FindObjectOfType<LevelManager>().isLevelOver) {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        
        if (currentHealth <= 0) {
            PlayerDies();
        }
    }

    void PlayerDies()
    {
        AudioSource.PlayClipAtPoint(playerDeath, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self);
        FindObjectOfType<LevelManager>().LoseLevel();
    }
}
