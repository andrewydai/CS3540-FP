using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateAnimateTrigger : MonoBehaviour
{
    public AudioClip slamSFX;
    public float bounceForce = 5;

    public void PlaySlamNoise()
    {
        AudioSource.PlayClipAtPoint(slamSFX, transform.position);
    }

    public void ToggleAttack()
    {
        gameObject.GetComponentInParent<PlateBehavior>().ToggleAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit player");
        }
        /*
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyBehavior>().
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            Vector3 forceDirection = transform.position - other.transform.position;
            other.GetComponent<Rigidbody>().AddForce(-forceDirection * bounceForce);
        }
    }
}
