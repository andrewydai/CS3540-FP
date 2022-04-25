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
        var bgobj = GameObject.FindGameObjectWithTag("IsBossLevel");
        if (bgobj != null)
        {
            if (BossLevelManager.isLevelOver)
            {
                return;
            }
        }
        else if (LevelManager.isLevelOver)
        {
            return;
        }

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
            var playerBounce = other.GetComponent<PlayerBounceBehavior>();
            playerHealth.TakeDamage(parent.damageAmount);
            playerBounce.BouncePlayer(parent.transform.forward, parent.transform.position);
        }
        /*
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyBehavior>().
        }*/
    }
}
