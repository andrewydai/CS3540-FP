using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLights : MonoBehaviour
{
    private Light thisLight;
    private bool isFlickering = false;
    private float timeDelay;
    public float delay;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        thisLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(!isFlickering && currentTime > delay)
        {
            StartCoroutine(FlickerLight());
        }
    }

    IEnumerator FlickerLight()
    {
        isFlickering = true;
        thisLight.enabled = false;
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);

        thisLight.enabled = true;
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
        currentTime = 0;
    }
}
