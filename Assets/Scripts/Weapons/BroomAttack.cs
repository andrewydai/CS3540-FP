using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomAttack : MonoBehaviour
{
    public int damage = 10;
    public AudioClip broomHit;
    bool isAttacking;
    List<int> hitObjects;
    Transform player;

    void Start()
    {
        hitObjects = new List<int>();
        isAttacking = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetAttacking()
    {
        isAttacking = true;
        Invoke("UnsetAttacking", 0.5f);
    }

    void UnsetAttacking()
    {
        isAttacking = false;
        hitObjects.Clear();
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
        if (hitObjects.Contains(gobj.GetInstanceID()))
        {
            return;
        }

        if (gobj.CompareTag("Enemy") && isAttacking)
        {
            hitObjects.Add(gobj.GetInstanceID());
            gobj.GetComponent<EnemyBehavior>().TakeDamage(damage);
            var bounceBehavior = gobj.GetComponent<EnemyBounceBehavior>();
            if (bounceBehavior != null)
            {
                bounceBehavior.BounceEnemy(player.position);
            }
            AudioSource.PlayClipAtPoint(broomHit, player.position);
        }
        else if (gobj.CompareTag("Boss") && isAttacking)
        {
            hitObjects.Add(gobj.GetInstanceID());
            gobj.GetComponentInParent<BossBehavior>().TakeDamage(damage);
            AudioSource.PlayClipAtPoint(broomHit, player.position);
        }
    }
}
