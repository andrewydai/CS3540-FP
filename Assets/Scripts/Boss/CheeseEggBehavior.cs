using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseEggBehavior : MonoBehaviour
{
    public GameObject cheeseWayPointPrefab;
    public float explosionRadius;
    public int damageAmount;
    public Slider timerSlider;
    public int spawnTime;
    public AudioClip popSFX;
    bool startedTimer = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(startedTimer)
        {
            return;
        }

        startedTimer = true;
        timerSlider.maxValue = spawnTime;
        timerSlider.value = spawnTime;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        int currentTime = spawnTime;
        GetComponent<AudioSource>().Play();
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            timerSlider.value = currentTime;
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<Animator>().SetBool("Explode", true);
        yield return null;
    }

    void PlayPop()
    {
        AudioSource.PlayClipAtPoint(popSFX, transform.position);
    }

    void Explode()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if(Vector3.Distance(player.transform.position, transform.position) < explosionRadius)
        {
            var playerHealth = player.GetComponent<PlayerHealth>();
            var playerBounce = player.GetComponent<PlayerBounceBehavior>();
            playerHealth.TakeDamage(damageAmount);
            playerBounce.BouncePlayer(player.transform.position - transform.position, transform.position);
        }
        Destroy(gameObject);
    }
}
