using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    public Slider yellowHealthSlider;
    public AudioClip playerDeath;
    int currentHealth;
    PostProcessVolume postfx;

    // Start is called before the first frame update
    void Start()
    {
        postfx = Camera.main.GetComponent<PostProcessVolume>();
        currentHealth = startingHealth;
        yellowHealthSlider.value = startingHealth;
        yellowHealthSlider.maxValue = startingHealth;
        healthSlider.value = startingHealth;
        healthSlider.maxValue = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < 20)
        {
            postfx.weight = Mathf.Clamp(postfx.weight + Time.deltaTime, 0, 1);
        }
        else
        {
            postfx.weight = Mathf.Clamp(postfx.weight - Time.deltaTime, 0, 1);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (LevelManager.isLevelOver)
        {
            return;
        }

        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
            StartCoroutine(LerpYellowHealth());
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    IEnumerator LerpYellowHealth()
    {
        yield return new WaitForSeconds(2f);
        float t = 1;
        while (yellowHealthSlider.value > currentHealth)
        {
            yellowHealthSlider.value = Mathf.MoveTowards(yellowHealthSlider.value, currentHealth, t);
            yield return null;
        }
        yield return null;
    }

    void PlayerDies()
    {
        AudioSource.PlayClipAtPoint(playerDeath, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self);
        FindObjectOfType<LevelManager>().LoseLevel();
    }

    public void boostHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthSlider.value = currentHealth;
    }
}
