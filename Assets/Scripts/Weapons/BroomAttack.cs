using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomAttack : MonoBehaviour
{
    public int damage = 10;
    public AudioClip broomHit;
    bool isAttacking;
    Transform player;

    void Start()
    {
        isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetAttacking()
    {
        isAttacking = true;
        Invoke("UnsetAttacking", 0.75f);
    }

    void UnsetAttacking()
    {
        isAttacking = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject gobj = other.gameObject;
        if(gobj.CompareTag("Enemy") && isAttacking)
        {
            gobj.GetComponent<EnemyBehavior>().TakeDamage(damage);
            gobj.GetComponent<EnemyBounceBehavior>().BounceEnemy(player.position);
            AudioSource.PlayClipAtPoint(broomHit, player.position);
        }
        else if(gobj.CompareTag("Boss"))
        {
            gobj.GetComponentInParent<BossBehavior>().TakeDamage(damage);
        }
    }
}
