using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public enum AIState
    {
        Patrol,
        ChasePlayer,
        Death,
        AttackPlayer,
    }
    public AIState aiState;

    public enum AIBehaviorMode
    {
        Normal,
        Alert,
        Engaging
    }
    public AIBehaviorMode aiBehaviorMode;


    public GameObject[] waypoints;
    private int currWaypoint;
    [SerializeField] public GameObject player;
    [SerializeField] public float detectRange = 3f;
    [SerializeField] public float lostRangeWithHidden = 5f;
    [SerializeField] public float lostRangeWithoutHidden = 8f;
    private VelocityReporter target;
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] float timeToKill = 0.5f;
    private float timer;
    [SerializeField] float timeToDestory = 1f;
    [SerializeField] float pauseTime = 1f;

    [SerializeField] GameObject QuestionMark;
    [SerializeField] GameObject ExclamationMark;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //Debug.Log("player: " + player);
        target = player.GetComponent<VelocityReporter>();
        aiState = AIState.Patrol;
        aiBehaviorMode = AIBehaviorMode.Normal;

        currWaypoint = -1;
        setNextWaypoint();

        timer = timeToKill;
    }

    void Update()
    {
        float targetDistance = Vector3.Distance(transform.position, player.transform.position);
        bool isHidden = player.GetComponent<PlayerScript>().isHidden;

        if (aiBehaviorMode == AIBehaviorMode.Alert)
        {
            return;
        }

        if (aiBehaviorMode == AIBehaviorMode.Normal && targetDistance <= detectRange && !isHidden)
        {
            // Stop moving
            agent.isStopped = true;
            animator.SetBool("Walk Forward", false);

            // Set State
            isAlert();


            // start alert coroutine
            StartCoroutine(PauseForSeconds(pauseTime));
        }

        

        // Check if player is in range
        //if (aiState == AIState.Patrol && targetDistance <= detectRange && !isHidden)
        //{
        //    aiState = AIState.ChasePlayer;
        //}

        //if (aiState == AIState.Patrol)
        //{
        //    if (targetDistance <= detectRange && !isHidden && aiState == AIState.Patrol)
        //    {
        //        aiState = AIState.ChasePlayer;
        //    }
        //    else
        //    {
        //        aiState = AIState.Patrol;
        //    }
        //}

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
                chasingTarget(targetDistance, isHidden);
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
    private void isAlert()
    {
        aiBehaviorMode = AIBehaviorMode.Alert;
        QuestionMark.GetComponent<MeshRenderer>().enabled = true;
        ExclamationMark.GetComponent<MeshRenderer>().enabled = false;
    }

    private void isEngaging()
    {
        aiState = AIState.ChasePlayer;
        aiBehaviorMode = AIBehaviorMode.Engaging;
        QuestionMark.GetComponent<MeshRenderer>().enabled = false;
        ExclamationMark.GetComponent<MeshRenderer>().enabled = true;
    }

    private void isNormal()
    {
        aiState = AIState.Patrol;
        aiBehaviorMode = AIBehaviorMode.Normal;
        QuestionMark.GetComponent<MeshRenderer>().enabled = false;
        ExclamationMark.GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator PauseForSeconds(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        float targetDistance = Vector3.Distance(transform.position, player.transform.position);
        bool isHidden = player.GetComponent<PlayerScript>().isHidden;

        if (targetDistance <= detectRange && !isHidden)
        {
            isEngaging();
        }
        else
        {
            isNormal();
        }

        agent.isStopped = false;
    }


    private void setNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currWaypoint = (currWaypoint + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    //private void activateEngagingPlayerState()
    //{
    //    aiState = AIState.ChasePlayer;
    //    aiBehaviorMode = AIBehaviorMode.Engaging;
    //}

    private void chasingTarget(float targetDistance, bool cannotBeSeen)
    {
        // player lost NPC when distance greater than lost range.
        if ((targetDistance >= lostRangeWithHidden && cannotBeSeen) || (targetDistance >= lostRangeWithoutHidden))
        {
            isNormal();
            setNextWaypoint();
            return;
        }

        Vector3 targetVelocity = target.velocity;
        float lookaheadTime = Mathf.Clamp(targetDistance / agent.speed, 0, 1.0f);

        Vector3 extrapolatedPosition = player.transform.position + (targetVelocity * lookaheadTime);
        agent.SetDestination(extrapolatedPosition);
    }

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
