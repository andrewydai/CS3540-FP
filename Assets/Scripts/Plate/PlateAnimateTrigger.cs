using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateAnimateTrigger : MonoBehaviour
{
    public AudioClip slamSFX;
    PlateBehavior parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<PlateBehavior>();
    }

    public void PlaySlamNoise()
    {
        
        AudioSource.PlayClipAtPoint(slamSFX, transform.position);
    }

    public void ToggleAttack()
    {
        parent.ToggleAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && parent.isAttacking) {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(parent.damageAmount);
        }
        /*
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyBehavior>().
        }*/
    }
}
