using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateProjectileBehavior : MonoBehaviour
{
    public GameObject plateEnemy;
    bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Enemy") && !spawned)
        {
            spawned = true;
            Instantiate(plateEnemy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
