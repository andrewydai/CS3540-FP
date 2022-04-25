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
        if (LevelManager.isLevelOver)
        {
            return;
        }

        AudioSource.PlayClipAtPoint(slamSFX, transform.position);
        
    }

    public void ToggleAttack()
    {
        parent.ToggleAttack();
    }

    public void SetTrigger(bool isTrigger)
    {
        GetComponentsInChildren<BoxCollider>()[1].isTrigger = isTrigger;
    }
}
