using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    public enum FSMStates {
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
    public AudioClip toastShootSFX;
    public AudioClip plateShootSFX;
    public AudioClip plateSlamSFX;
    public AudioClip knifeAttackSFX;
    public AudioClip moveSFX;
    public int bossHealth;
    public int currentHealth;
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
        currentHealth = bossHealth;
        anim = GetComponentInChildren<Animator>();
        currentState = FSMStates.Ranged;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
        NavMeshPath navMeshPath = new NavMeshPath();
        agent.CalculatePath(player.transform.position, navMeshPath);
        canReachPlayer = navMeshPath.status != NavMeshPathStatus.PathPartial;
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
        if(rangedTimer < 0)
        {
            int animAttack = Random.Range(1, 3);
            anim.SetInteger("animState", animAttack);
            rangedTimer = rangedCooldown;
        }
        else
        {
            FacePlayer();
            rangedTimer -= Time.deltaTime;
        }

        if(canReachPlayer)
        {
            currentState = FSMStates.Melee;
        }
    }

    void UpdateMelee()
    {
        if (meleeTimer < 0)
        {
            isColliderDamaging = true;
            int animAttack = Random.Range(3, 5);
            anim.SetInteger("animState", animAttack);
            meleeTimer = meleeCooldown;
        }
        else
        {
            //FacePlayer();
            meleeTimer -= Time.deltaTime;
        }

        if (!canReachPlayer)
        {
            currentState = FSMStates.Ranged;
        }
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

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }

    void FacePlayer()
    {
        Vector3 directionToTarget = (player.transform.position - transform.position);
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
    }
}
