using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance { get; set; }
    private GameObject[] patrolPoints;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int currentPatrolPoint = 0;
    [SerializeField] private float waitAtPatrolPoint = 2.0f;
    private float waitAtPatrolPointCounter;
    private NavMeshAgent myNavMeshAgent;
    public Animator myAnimator;

    public enum AIState { IS_IDLE, IS_PATROLLING, IS_CHASING, IS_ATTACKTING, IS_DEAD}
    public static AIState CurrentAIState { get; set; }
    [SerializeField] private float chaseRange = 10.0f;
    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] private float waitBetweenAttacks = 4.0f;
    [SerializeField] private float DeadedWaitDelet = 10.0f;
    private float waitBetweenAttacksCounter;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        //myNavMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
        CurrentAIState = AIState.IS_IDLE;
        waitAtPatrolPointCounter = waitAtPatrolPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
       if(CurrentAIState == AIState.IS_DEAD)
        {
            myAnimator.SetBool("Moving", false);
            if(DeadedWaitDelet>0.0f)
            DeadedWaitDelet -= Time.deltaTime;
            else
            {
                Enemy.SetActive(false);
            }
        }
        else if (CurrentAIState == AIState.IS_IDLE)
        {
            myAnimator.SetBool("Moving", false);
            if (waitAtPatrolPointCounter > 0.0f)
            {
                waitAtPatrolPointCounter -= Time.deltaTime;
            }
            else
            {
                myNavMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
                CurrentAIState = AIState.IS_PATROLLING;
            }
            if(distanceToPlayer<=chaseRange)
            {
                CurrentAIState = AIState.IS_CHASING;
            }
        }else if(CurrentAIState == AIState.IS_PATROLLING)

        {
            myAnimator.SetBool("Moving", true);
            if (myNavMeshAgent.remainingDistance <= 1.0f)
            {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
                waitAtPatrolPointCounter = waitAtPatrolPoint;
                //myNavMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
                CurrentAIState = AIState.IS_IDLE;
            }
            if (distanceToPlayer <= chaseRange)
            {
                CurrentAIState = AIState.IS_CHASING;
            }
        }else if (CurrentAIState == AIState.IS_CHASING)
        {
            myAnimator.SetBool("Moving", true);
            myNavMeshAgent.SetDestination(PlayerController.Instance.transform.position);
            if(distanceToPlayer>chaseRange)
            {
                myNavMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
                CurrentAIState = AIState.IS_PATROLLING;
            }else if(distanceToPlayer<=attackRange)
            {
                myNavMeshAgent.velocity = Vector3.zero;
                myNavMeshAgent.isStopped = true;
                myAnimator.SetBool("Moving", false);
                myAnimator.SetTrigger("Attack");
                waitBetweenAttacksCounter = waitBetweenAttacks;
                CurrentAIState = AIState.IS_ATTACKTING;
            }
        }else if(CurrentAIState == AIState.IS_ATTACKTING)
        {
            transform.LookAt(PlayerController.Instance.transform, Vector3.up);
            transform.rotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
            waitBetweenAttacksCounter -= Time.deltaTime;
            if(waitBetweenAttacksCounter<=0.0f)
            {
                if(distanceToPlayer<=attackRange)
                {
                    myAnimator.SetTrigger("Attack");
                    waitBetweenAttacksCounter = waitBetweenAttacks;
                }
                else
                {
                    myNavMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
                    myNavMeshAgent.velocity = Vector3.forward;
                    myNavMeshAgent.isStopped = false;
                    CurrentAIState = AIState.IS_PATROLLING;
                }
            }
        }



}
}
