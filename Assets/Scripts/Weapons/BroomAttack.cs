using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomAttack : MonoBehaviour
{
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject gobj = other.gameObject;
        if(gobj.CompareTag("Enemy"))
        {
            gobj.GetComponent<EnemyBehavior>().TakeDamage(damage);
        }
    }
}
