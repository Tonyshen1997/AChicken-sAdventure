using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testAi : MonoBehaviour
{


    public enum AIState
    {
        Patrol,
        ChasePlayer,
        Death,
        AttackPlayer
        // TODO more behavior
    }
    public AIState aiState;

    public GameObject[] waypoints;
    private int currWaypoint;
    [SerializeField] public GameObject player;
    [SerializeField] public float detectRange = 3f;
    private VelocityReporter target;
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] float timeToKill = 0.5f;
    private float timer;
    [SerializeField] float timeToDestory = 1f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //Debug.Log("player: " + player);
        target = player.GetComponent<VelocityReporter>();
        aiState = AIState.Patrol;

        currWaypoint = -1;
        setNextWaypoint();

        timer = timeToKill;
    }


    void Update()
    {
        float targetDistance = Vector3.Distance(transform.position, player.transform.position);

        // Check if player is in range
        if (aiState == AIState.Patrol)
        {
            if (targetDistance <= detectRange) aiState = AIState.ChasePlayer;
            else aiState = AIState.Patrol;
        }



        switch (aiState)
        {
            case AIState.Patrol:
                if (agent.remainingDistance <= 0.5 && !agent.pathPending)
                {
                    setNextWaypoint();
                }
                animator.SetBool("Walk Forward", true);
                break;

            case AIState.ChasePlayer:
                chasingTarget(targetDistance);
                animator.SetBool("Walk Forward", true);
                break;

            case AIState.Death:
                animator.SetBool("Walk Forward", false);
                animator.SetTrigger("Die");
                Destroy(gameObject, timeToDestory);
                break;

            case AIState.AttackPlayer:
                animator.SetBool("Walk Forward", false);
                animator.SetTrigger("Attack 01");
                break;
        }
    }

    private void setNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currWaypoint = (currWaypoint + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    private void chasingTarget(float targetDistance)
    {
        Vector3 targetVelocity = target.velocity;
        float lookaheadTime = Mathf.Clamp(targetDistance / agent.speed, 0, 1.0f);

        Vector3 extrapolatedPosition = player.transform.position + (targetVelocity * lookaheadTime);
        agent.SetDestination(extrapolatedPosition);
    }

    //private void OnTriggerEnter(Collider c)
    //{
    //    if (c.gameObject.tag == "Player")
    //    {
    //        aiState = AIState.Death;
    //    }
    //}

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                aiState = AIState.Death;
            }
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            timer = timeToKill;
        }
    }

    public void SetAttack(bool state)
    {
        if (state) aiState = AIState.AttackPlayer;
        else aiState = AIState.ChasePlayer;
    }
}