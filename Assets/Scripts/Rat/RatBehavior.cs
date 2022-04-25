using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBehavior : MonoBehaviour
{
    public enum FSMStates {
        Patrol,
        Attack
    }

    public FSMStates currentState;

    public float patrolSpeed;
    public float attackSpeed;
    public float attackDistance = 10f;
    public int damage = 10;
    
    GameObject player;
    NavMeshAgent agent;
    GameObject[] cheesePoints;

    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        cheesePoints = GameObject.FindGameObjectsWithTag("Cheesepoint");
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        currentState = FSMStates.Patrol;
        FindNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState) {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
        }
    }

    // State Updates

    void UpdatePatrolState()
    {
        agent.stoppingDistance = 1.5f;
        agent.speed = patrolSpeed;

        if (Vector3.Distance(transform.position, nextDestination) <= agent.stoppingDistance) {
            FindNextPoint();
        } else if (distanceToPlayer <= attackDistance) {
            currentState = FSMStates.Attack;
        }

        //FaceTarget(nextDestination);
        
        agent.SetDestination(nextDestination);
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;

        agent.stoppingDistance = 0;
        agent.speed = attackSpeed;

        if (distanceToPlayer > attackDistance) {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        //FaceTarget(nextDestination);
        
        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void FindNextPoint()
    {
        nextDestination = cheesePoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % cheesePoints.Length;

        agent.SetDestination(nextDestination);
    }

    /*
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    */

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            this.GetComponent<EnemyBounceBehavior>().BounceEnemy(player.transform.position);
        }
    }
    
}
