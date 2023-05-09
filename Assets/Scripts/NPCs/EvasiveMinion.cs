using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent), typeof(BoxCollider))]
public class EvasiveMinion : MonoBehaviour, Minion
{
    // fine tune
    [SerializeField] float timeToDisappear = 1f;
    [SerializeField] float evadeDistance = 3f;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float wanderRadius = 20f;


    // setup
    [SerializeField] GameObject player;

    // private fields
    private enum AIState
    {
        evadingPlayer,
        wondering
    }
    private AIState aiState;
    private Animator animator;
    private NavMeshAgent agent;
    private VelocityReporter velocityReporter;
    //private float timer;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        velocityReporter = player.GetComponent<VelocityReporter>();
        aiState = AIState.wondering;
    }

    void Update()
    {
        StateMachineBehavior();
    }

    private void StateMachineBehavior()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < detectionRadius)
        {
            aiState = AIState.evadingPlayer;
        } else
        {
            aiState = AIState.wondering;
        }



            switch (aiState)
        {
            case AIState.wondering:
                wondering();
                break;
            case AIState.evadingPlayer:
                evadingPlayer();
                break;
        }
    }

    private void wondering()
    {
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, wanderRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }

    private void evadingPlayer()
    {
        animator.SetBool("Run", true);
        Vector3 playerDir = transform.position - player.transform.position;
        Vector3 targetPosition = transform.position + playerDir.normalized * evadeDistance;
        agent.SetDestination(targetPosition);
    }

    public void setTarget(GameObject target)
    {
        player = target;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Die");
            agent.isStopped = true;
        }
    }


    public void Death()
    {
        DemoCamera cameraController = FindObjectOfType<DemoCamera>();
        if (cameraController == null) Debug.Log("not found");
        CatapultSystemController catapultSystemController = FindObjectOfType<CatapultSystemController>();
        cameraController.MoveToPosition(catapultSystemController.camPos);
        catapultSystemController.Raise();

        Boss_AI boss = FindObjectOfType<Boss_AI>();
        boss.stopSpawning = true;
        Destroy(gameObject, timeToDisappear);
    }


}
