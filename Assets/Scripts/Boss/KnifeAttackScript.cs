using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttackScript : MonoBehaviour
{
    public int damageAmount;
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if(go.CompareTag("Player") && FindObjectOfType<BossBehavior>().isColliderDamaging)
        {
            go.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }
}
