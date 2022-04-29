using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public int bossHealth;
    public int currentHealth;
    public ParticleSystem deathFX;
    public Slider bossHealthSlider;
    public Slider bossYellowHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = bossHealth;
        bossHealthSlider.maxValue = bossHealth;
        bossHealthSlider.value = bossHealth;
        bossYellowHealthSlider.maxValue = bossHealth;
        bossYellowHealthSlider.value = bossHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        if (LevelManager.isLevelOver)
        {
            return;
        }

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Death();
            FindObjectOfType<LevelManager>().WinLevel();
            bossHealthSlider.value = 0;
            bossYellowHealthSlider.value = 0;
        }
        else
        {
            bossHealthSlider.value = currentHealth;
            StartCoroutine(LerpYellowHealth());
        }
    }

    IEnumerator LerpYellowHealth()
    {
        yield return new WaitForSeconds(2f);
        float t = 1;
        while (bossYellowHealthSlider.value > currentHealth)
        {
            bossYellowHealthSlider.value = Mathf.MoveTowards(bossYellowHealthSlider.value, currentHealth, t);
            yield return null;
        }
        yield return null;
    }

    public void Death()
    {
        Instantiate(deathFX, transform.position, Quaternion.Euler(new Vector3(-70.063f, 124.382f, -131.329f)));
        Destroy(gameObject);
    }
}
