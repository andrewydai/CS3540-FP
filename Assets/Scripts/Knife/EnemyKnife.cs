using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnife : MonoBehaviour
{
    public enum FSMState
    {
        Asleep,
        Awake,
        Attack
    }
    public FSMState currentstate;
    public float awakeRange;
    public float attackRange;
    public GameObject player;
    public float lookSpeed;
    public float attackForce;
    public int damage;
    public float cooldownTime;
    public AudioClip hitSFX;
    public AudioClip attackSFX;
    Animator childAnimator;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentstate = FSMState.Asleep;
        childAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponentsInChildren<Collider>()[0]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(LevelManager.isLevelOver)
        {
            return;
        }

        switch (currentstate)
        {
            case FSMState.Asleep:
                UpdateAsleep();
                break;
            case FSMState.Awake:
                UpdateAwake();
                break;
            case FSMState.Attack:
                UpdateAttack();
                break;
        }
    }

    // gravity is on and collisions are on
    void UpdateAsleep()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < awakeRange)
        {
            rb.useGravity = false;
            rb.detectCollisions = false;
            childAnimator.SetInteger("AnimState", 1);
        }
    }

    // gravity is off and collisions are off
    void UpdateAwake()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > awakeRange)
        {
            rb.useGravity = true;
            rb.detectCollisions = true;
            childAnimator.SetInteger("AnimState", 2);
            return;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && IsFacingPlayer())
        {
            rb.detectCollisions = true;
            childAnimator.SetInteger("AnimState", 3);
            return;
        }

        FacePlayer();
    }

    void UpdateAttack()
    {
        if (Vector3.Distance(rb.velocity, Vector3.zero) == 0)
        {
            Invoke("FallAsleep", cooldownTime);
        }

    }

    public void FallAsleep()
    {
        childAnimator.SetInteger("AnimState", 0);
        currentstate = FSMState.Asleep;
    }

    public void WakeUp()
    {
        currentstate = FSMState.Awake;
    }

    public void Attack()
    {
        AudioSource.PlayClipAtPoint(attackSFX, transform.position);
        currentstate = EnemyKnife.FSMState.Attack;
        rb.AddForce(transform.forward * attackForce);
    }

    void FacePlayer()
    {
        Vector3 directionToTraget = (player.transform.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(directionToTraget.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(currentstate == FSMState.Attack)
        {
            rb.useGravity = true;
            rb.detectCollisions = true;
        }
    }

    bool IsFacingPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }
}
