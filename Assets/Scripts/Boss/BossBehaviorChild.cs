using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviorChild : MonoBehaviour
{
    public int smashDamageAmount;
    BossBehavior parentScript;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        parentScript = GetComponentInParent<BossBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

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
            go.GetComponent<PlayerHealth>().TakeDamage(smashDamageAmount);
        }
    }
}
