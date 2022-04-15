using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeAttack : MonoBehaviour
{
    public int damage;
    public GameObject projectile;
    public AudioClip attackSFX;

    GameObject cameraMount;

    // Start is called before the first frame update
    void Start()
    {
        cameraMount = GameObject.FindGameObjectWithTag("CameraMount");
    }

    public void Attack()
    {
        Transform playerTransform = cameraMount.transform;

        AudioSource.PlayClipAtPoint(attackSFX, transform.position);
        Instantiate(projectile, playerTransform.position + playerTransform.forward, playerTransform.rotation);
    }
}
