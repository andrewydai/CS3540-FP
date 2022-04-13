using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceBehavior : MonoBehaviour
{
    CharacterController player;
    Vector3 impact = Vector3.zero;
    public float impactStrength = 2f;
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
    }

    public void BouncePlayer(Vector3 enemyDirection, Vector3 enemyPosition)
    {
        Vector3 hitDirection = enemyPosition - transform.position;
        impact += hitDirection.normalized * impactStrength;
    }
}
