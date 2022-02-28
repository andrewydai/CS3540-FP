using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeProjectile : MonoBehaviour
{
    public float projectileSpeed = 100;
    public GameObject spongeWeapon;
    void Awake()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed, ForceMode.VelocityChange);
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gobj = collision.gameObject;
        if (gobj.CompareTag("Enemy"))
        {
            gobj.GetComponent<EnemyBehavior>().TakeDamage(spongeWeapon.GetComponent<SpongeAttack>().damage);
            
        }
        Destroy(gameObject);
    }
}
