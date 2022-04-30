using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RattusBossBehavior : MonoBehaviour
{
    public CheeseSpawnerBehavior cheeseSpawner;
    public GameObject player;
    NavMeshAgent agent;
    public bool isColliderDamaging;
    public bool isGoldenTouch;
    public float attackRange;
    public float rangedCooldown;
    public float meleeCooldown;
    public int meleeDamage;
    public float lookSpeed;
    public float laserSpeed;
    public GameObject laserPrefab;
    public Transform leftEye;
    public Transform rightEye;
    public Vector3 playerPosLaser;
    public AudioClip laserSFX;
    public AudioClip cheeseRoarSFX;
    public AudioClip chargeRoarSFX;
    public AudioClip basicRoarSFX;
    float attackTimer;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
        anim = GetComponentInChildren<Animator>();
        cheeseSpawner = FindObjectOfType<CheeseSpawnerBehavior>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.isLevelOver)
        {
            return;
        }
        NavMeshPath navMeshPath = new NavMeshPath();
        agent.CalculatePath(player.transform.position, navMeshPath);
        bool canReachPlayer = navMeshPath.status != NavMeshPathStatus.PathPartial && player.GetComponent<CharacterController>().isGrounded;
        agent.SetDestination(player.transform.position);
        if (!canReachPlayer)
        {
            FacePlayer();
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        if (canReachPlayer && Vector3.Distance(player.transform.position, transform.position) < attackRange)
        {
            if(attackTimer < 0)
            {
                int animState = Random.Range(3, 6);
                isColliderDamaging = true;
                isGoldenTouch = animState == 5;
                anim.SetInteger("AnimState", animState);
                attackTimer = meleeCooldown;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (attackTimer < 0)
            {
                int animState = Random.Range(1, 3);
                anim.SetInteger("AnimState", animState);
                if (animState == 1)
                {
                    attackTimer = 9;
                }
                else
                {
                    attackTimer = rangedCooldown;
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    public void SpawnCheese()
    {
        cheeseSpawner.SpawnCheese();
    }

    public void FireLasers()
    {
        var leftLaser = Instantiate(laserPrefab, leftEye.position, leftEye.rotation);
        var rightLaser = Instantiate(laserPrefab, rightEye.position, rightEye.rotation);
        leftLaser.GetComponent<Rigidbody>().AddForce((playerPosLaser - leftLaser.transform.position) * laserSpeed, ForceMode.VelocityChange);
        rightLaser.GetComponent<Rigidbody>().AddForce((playerPosLaser - rightLaser.transform.position) * laserSpeed, ForceMode.VelocityChange);
    }

    void FacePlayer()
    {
        Vector3 directionToTarget = (player.transform.position - transform.position);
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
    }
}
