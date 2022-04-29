using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RattusBossBehaviorChild : MonoBehaviour
{
    RattusBossBehavior parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<RattusBossBehavior>();
    }

    public void SpawnCheese()
    {
        parent.SpawnCheese();
    }

    public void PlayBasicRoarSFX()
    {
        AudioSource.PlayClipAtPoint(parent.basicRoarSFX, transform.position);
    }

    public void PlayCheeseRoarSFX()
    {
        AudioSource.PlayClipAtPoint(parent.cheeseRoarSFX, Camera.main.transform.position);
    }

    public void PlayChargeRoarSFX()
    {
        AudioSource.PlayClipAtPoint(parent.chargeRoarSFX, transform.position);
    }

    public void PlayLaserSFX()
    {
        AudioSource.PlayClipAtPoint(parent.laserSFX, Camera.main.transform.position);
    }

    public void SetPlayerPosition()
    {
        parent.playerPosLaser = parent.player.transform.position;
    }
    public void FireLasers()
    {
        parent.FireLasers();
    }

    public void SetIdle()
    {
        GetComponent<Animator>().SetInteger("AnimState", 0);
        parent.transform.position = transform.position;
        parent.isColliderDamaging = false;
        parent.isGoldenTouch = false;
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player") && parent.isColliderDamaging)
        {
            if(parent.isGoldenTouch)
            {
                go.GetComponent<PlayerGoldTouch>().Freeze();
            }
            parent.isColliderDamaging = false;
            parent.isGoldenTouch = false;
            var playerHealth = go.GetComponent<PlayerHealth>();
            var playerBounce = go.GetComponent<PlayerBounceBehavior>();
            playerHealth.TakeDamage(parent.meleeDamage);
            playerBounce.BouncePlayer(parent.transform.forward, parent.transform.position);
        }
    }
}
