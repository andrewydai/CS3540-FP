using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public GameObject parent;
    PlateBehavior parentBehavior;
    // Start is called before the first frame update
    void Start()
    {
        parentBehavior = parent.GetComponent<PlateBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && parentBehavior.isAttacking)
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            var playerBounce = other.GetComponent<PlayerBounceBehavior>();
            playerHealth.TakeDamage(parentBehavior.damageAmount);
            playerBounce.BouncePlayer(parentBehavior.transform.forward, parentBehavior.transform.position);
        }
    }
}
