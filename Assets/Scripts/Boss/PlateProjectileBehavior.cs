using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateProjectileBehavior : MonoBehaviour
{
    public GameObject plateEnemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(plateEnemy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
