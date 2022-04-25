using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounceBehavior : MonoBehaviour
{
    Transform enemy;
    Vector3 impact = Vector3.zero;
    public float impactStrength = 2f;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (impact.magnitude > 0.2f) enemy.position += (-impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, Time.deltaTime * 2f);
    }

    public void BounceEnemy(Vector3 hitPosition)
    {
        Vector3 hitDirection = hitPosition - transform.position;
        impact += hitDirection.normalized * impactStrength;
    }
}