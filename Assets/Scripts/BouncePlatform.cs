using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerBounce = other.GetComponent<PlayerBounceBehavior>();
            playerBounce.BouncePlatform(transform.up);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerBounce = collision.gameObject.GetComponent<PlayerBounceBehavior>();
            playerBounce.BouncePlatform(transform.up);
        }
    }
}
