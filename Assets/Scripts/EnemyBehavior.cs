using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int health = 25;
    public AudioClip deathSFX;
    public static int enemyCount = 0;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount++;
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
        }
        else
        {
            currentHealth = 0;
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            FindObjectOfType<LevelManager>().WinLevel();
        }
    }
}
