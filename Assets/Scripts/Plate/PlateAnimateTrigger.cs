using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateAnimateTrigger : MonoBehaviour
{
    public AudioClip slamSFX;

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
        if(other.CompareTag("Player"))
        {
            Debug.Log("Hit player");
        }
    }
}
