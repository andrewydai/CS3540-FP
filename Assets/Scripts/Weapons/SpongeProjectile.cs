using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeProjectile : MonoBehaviour
{
    public float projectileSpeed = 100;
    public GameObject spongeWeapon;
    bool hit;
    void Awake()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * projectileSpeed, ForceMode.VelocityChange);
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnEnter(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other.gameObject);
    }

    private void OnEnter(GameObject gobj)
    {
        if(hit)
        {
            return;
        }
        hit = true;
        if (gobj.CompareTag("Enemy"))
        {
            gobj.GetComponent<EnemyBehavior>().TakeDamage(spongeWeapon.GetComponent<SpongeAttack>().damage);
            
        }
        else if (gobj.CompareTag("Boss"))
        {
            gobj.GetComponentInParent<BossBehavior>().TakeDamage(spongeWeapon.GetComponent<SpongeAttack>().damage);
        }
        Destroy(gameObject);
    }
}
