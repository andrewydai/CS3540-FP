using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject target;
    public float aggroRange = 15f;
    public float attackRange = 2f;
    public float moveSpeed = 10f;
    public int damageAmount = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // when within range, look at and move towards player
        /*
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < aggroRange && !isAttacking)
        {
            // look at
            transform.LookAt(target.transform);

            // move towards player
            bool shouldWalk = Mathf.Sin(Time.time) > -0.5;
            if (shouldWalk)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed);
            }

            // when near, slam on player's position
            if(distance < attackRange)
            {
                // attack
                // play animation, call DealDamage();
                isAttacking = true;
                GetComponent<Animator>().SetTrigger("isAttacking");
            }
        }
        */
    }

    public void ToggleAttack()
    {
        FindObjectOfType<EnemyParentBehavior>().ToggleAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Hit player");
        }
    }
}
