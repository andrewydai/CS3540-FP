using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlatoBossBehavior : MonoBehaviour
{
    public enum FSMStates
    {
        Ranged, Melee
    }
    public FSMStates currentState = FSMStates.Ranged;
    public GameObject player;
    public float lookSpeed = 5;
    public float rangedCooldown = 5;
    public GameObject breadPrefab;
    public Transform leftBreadTransform;
    public Transform rightBreadTransform;
    public Transform plateSpawnTransform;
    public GameObject platePrefab;
    public float breadForce;
    public float plateForce;
    public float meleeCooldown;
    public bool isColliderDamaging = false;
    public AudioClip loadBreadSFX;
    public AudioClip toastShootSFX;
    public AudioClip plateShootSFX;
    public AudioClip plateSlamSFX;
    public AudioClip knifeAttackSFX;
    public AudioClip moveSFX;
    bool canReachPlayer;
    float meleeTimer;
    float rangedTimer = 0;
    Animator anim;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        anim = GetComponentInChildren<Animator>();
        currentState = FSMStates.Ranged;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isLevelOver)
        {
            return;
        }

        agent.SetDestination(player.transform.position);
        NavMeshPath navMeshPath = new NavMeshPath();
        agent.CalculatePath(player.transform.position, navMeshPath);
        canReachPlayer = navMeshPath.status != NavMeshPathStatus.PathPartial && player.GetComponent<CharacterController>().isGrounded;
        switch (currentState)
        {
            case FSMStates.Ranged:
                UpdateRanged();
                break;
            case FSMStates.Melee:
                UpdateMelee();
                break;
        }
    }

    void UpdateRanged()
    {
        if (rangedTimer < 0)
        {
            int animAttack = UnityEngine.Random.Range(1, 3);
            anim.SetInteger("animState", animAttack);
            rangedTimer = rangedCooldown;
        }
        else
        {
            FacePlayer();
            rangedTimer -= Time.deltaTime;
        }

        if (canReachPlayer)
        {
            currentState = FSMStates.Melee;
        }
    }

    void UpdateMelee()
    {
        if (meleeTimer < 0)
        {
            int animAttack = UnityEngine.Random.Range(3, 5);
            if (animAttack == 4)
            {
                isColliderDamaging = true;
                var colliders = new ArraySegment<BoxCollider>(GetComponentsInChildren<BoxCollider>(), 2, 3);
                foreach (BoxCollider bc in colliders)
                {
                    bc.isTrigger = true;
                }
            }
            anim.SetInteger("animState", animAttack);
            meleeTimer = meleeCooldown;
        }
        else
        {
            meleeTimer -= Time.deltaTime;
        }

        if (!canReachPlayer)
        {
            currentState = FSMStates.Ranged;
        }
    }

    public void LoadBread()
    {
        AudioSource.PlayClipAtPoint(loadBreadSFX, Camera.main.transform.position);
    }

    public void FireBread()
    {
        var leftBread = Instantiate(breadPrefab, leftBreadTransform.position, leftBreadTransform.rotation);
        var rightBread = Instantiate(breadPrefab, rightBreadTransform.position, rightBreadTransform.rotation);
        leftBread.GetComponent<Rigidbody>().AddForce((player.transform.position - leftBread.transform.position) * breadForce, ForceMode.VelocityChange);
        rightBread.GetComponent<Rigidbody>().AddForce((player.transform.position - rightBread.transform.position) * breadForce, ForceMode.VelocityChange);
        AudioSource.PlayClipAtPoint(toastShootSFX, Camera.main.transform.position);
    }

    public void FirePlate()
    {
        var plate = Instantiate(platePrefab, plateSpawnTransform.position, Quaternion.Euler(Vector3.zero));
        plate.GetComponent<Rigidbody>().AddForce(plateSpawnTransform.up * plateForce, ForceMode.VelocityChange);
        AudioSource.PlayClipAtPoint(plateShootSFX, Camera.main.transform.position);
    }

    void FacePlayer()
    {
        Vector3 directionToTarget = (player.transform.position - transform.position);
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
    }
}
