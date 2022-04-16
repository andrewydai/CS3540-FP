using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDeletion : MonoBehaviour
{
    public Vector3 boxSize;
    public Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool outOfBoundsX = transform.position.x < origin.x || transform.position.x > boxSize.x;
        bool outOfBoundsY = transform.position.y < origin.y || transform.position.y > boxSize.y;
        bool outOfBoundsZ = transform.position.z < origin.z || transform.position.z > boxSize.z;
        if (outOfBoundsX || outOfBoundsY || outOfBoundsZ)
        {
           GetComponent<EnemyBehavior>().TakeDamage(1000);
        }
    }
}
