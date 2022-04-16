using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadBehavior : MonoBehaviour
{
    public int damageAmount;
    bool damagedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        damagedPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            if(!damagedPlayer)
            {
                go.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                damagedPlayer = true;
                GetComponent<Renderer>().materials[1].color = new Color(0, 0, 0, 0);
            }
            else
            {
                var playerBounce = other.GetComponent<PlayerBounceBehavior>();
                playerBounce.BouncePlatform(transform.up);
            }
        }
    }
}
