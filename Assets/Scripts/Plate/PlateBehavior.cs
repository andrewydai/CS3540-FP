using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateBehavior : MonoBehaviour
{
    public GameObject target;
    public float aggroRange = 15f;
    public float attackRange = 2f;
    public float moveSpeed = 10f;
    public int damageAmount = 5;
    public float turnSpeed = 2;
    public bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // when within range, look at and move towards player
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < aggroRange && !isAttacking)
        {
            // look at
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

            // move towards player
            bool shouldWalk = Mathf.Sin(Time.time) > -0.5;
            if (shouldWalk)
            {
                Vector3 targetWithoutY = Vector3.Scale(Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * moveSpeed), new Vector3(1, 0, 1));
                transform.position = targetWithoutY + new Vector3(0, transform.position.y, 0);
            }

            // when near, slam on player's position
            if (distance < attackRange)
            {
                // attack
                // play animation, call DealDamage();
                isAttacking = true;
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponentInChildren<Animator>().SetTrigger("isAttacking");
                GetComponentInChildren<BoxCollider>().isTrigger = true;
            }
        }
    }

    public void ToggleAttack()
    {
        isAttacking = false;
        transform.position += transform.forward * 1.6f;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponentInChildren<BoxCollider>().isTrigger = false;
    }

}
