using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatoBossBehaviorChild : MonoBehaviour
{
    public int smashDamageAmount;
    PlatoBossBehavior parentScript;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        parentScript = GetComponentInParent<PlatoBossBehavior>();
    }

    void LoadBread()
    {
        parentScript.LoadBread();
    }

    void FireBread()
    {
        parentScript.FireBread();
    }

    void FirePlate()
    {
        parentScript.FirePlate();
    }

    void SetIdle()
    {
        anim.SetInteger("animState", 0);
        var colliders = new ArraySegment<BoxCollider>(GetComponents<BoxCollider>(), 2, 3);
        foreach (BoxCollider bc in colliders)
        {
            bc.isTrigger = false;
        }
        parentScript.isColliderDamaging = false;
        AudioSource.PlayClipAtPoint(parentScript.moveSFX, Camera.main.transform.position);
    }

    void PlaySlamSoundEffect()
    {
        AudioSource.PlayClipAtPoint(parentScript.plateSlamSFX, Camera.main.transform.position);
    }

    void PlaySiceSoundEffect()
    {
        AudioSource.PlayClipAtPoint(parentScript.knifeAttackSFX, Camera.main.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player") && parentScript.isColliderDamaging)
        {
            parentScript.isColliderDamaging = false;
            var colliders = new ArraySegment<BoxCollider>(GetComponents<BoxCollider>(), 2, 3);
            foreach (BoxCollider bc in colliders)
            {
                bc.isTrigger = false;
            }
            go.GetComponent<PlayerHealth>().TakeDamage(smashDamageAmount);
        }
    }
}
