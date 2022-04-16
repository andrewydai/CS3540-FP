using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMakeSound : MonoBehaviour
{
    public float interval = 10f;
    
    private AudioSource sfx;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();   
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= interval)
        {
            sfx.Play();
            timeElapsed = 0;
        }
    }
}
