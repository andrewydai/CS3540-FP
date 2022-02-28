using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 0;
    public AudioClip deathSFX;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth >= damageAmount)
        {
            currentHealth -= damageAmount;
        } else
        {
            currentHealth = 0;
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
            Destroy(gameObject);
        }
    }
}
