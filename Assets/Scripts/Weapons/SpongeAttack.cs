using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAttack : MonoBehaviour
{
    public int damage;
    public GameObject projectile;
    public AudioClip attackSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack()
    {
        AudioSource.PlayClipAtPoint(attackSFX, transform.position);
        Transform cameraTransform = Camera.main.transform;
        Instantiate(projectile, cameraTransform.position + cameraTransform.forward * 2, cameraTransform.rotation);
    }
}
