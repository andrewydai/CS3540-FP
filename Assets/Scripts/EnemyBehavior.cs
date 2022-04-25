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
    public GameObject heartPrefab;
    public Color damageColor;
    public float damagedTime = 0.5f;
    
    private Renderer enemyRenderer;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount++;
        currentHealth = health;
        healthSlider.maxValue = health;
        healthSlider.value = currentHealth;
        enemyRenderer = GetComponentInChildren<Renderer>();
        originalColor = enemyRenderer.material.color;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth != 0)
        {
            // flash red on hit
            StartCoroutine(FlashDamagedColor());
            
            if (currentHealth > damageAmount)
            {
                currentHealth -= damageAmount;
            }
            else
            {
                currentHealth = 0;
                Instantiate(heartPrefab, transform.position, new Quaternion(0, 0, 0, 0));
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

    IEnumerator FlashDamagedColor()
    {
        enemyRenderer.material.color = damageColor;
        yield return new WaitForSeconds(damagedTime);
        enemyRenderer.material.color = originalColor;
    }
}
