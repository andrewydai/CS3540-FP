using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceBehavior : MonoBehaviour
{
    CharacterController player;
    Vector3 impact = Vector3.zero;
    Vector3 platformImpact = Vector3.zero;
    public float impactStrength = 2f;
    public float platformBounceStrength = 40f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (impact.magnitude > 0.2f) player.Move(-impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, Time.deltaTime * 2f);

        if (platformImpact.magnitude > 0.2f) player.Move(-platformImpact * Time.deltaTime);

        platformImpact = Vector3.Lerp(platformImpact, Vector3.zero, Time.deltaTime * 2f);
    }

    public void BouncePlayer(Vector3 enemyDirection, Vector3 enemyPosition)
    {
        Vector3 hitDirection = enemyPosition - transform.position;
        impact += hitDirection.normalized * impactStrength;
    }

    public void BouncePlatform(Vector3 platformDirection)
    {
        Debug.Log("Player on bounce platform");
        platformImpact += -platformDirection * platformBounceStrength;
    }
}
