using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAttack : MonoBehaviour
{
    public int damage;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack()
    {
        Transform cameraTransform = Camera.main.transform;
        Instantiate(projectile, cameraTransform.position + cameraTransform.forward * 2, cameraTransform.rotation);
    }
}
