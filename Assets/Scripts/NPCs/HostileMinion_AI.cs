using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface Minion
{
    public void setTarget(GameObject target);
}



[RequireComponent(typeof(Animator), typeof(NavMeshAgent), typeof(BoxCollider))]
public class HostileMinion_AI : MonoBehaviour, Minion
{
    // fine tune
    [SerializeField] float deathCountDown = 4f;
    [SerializeField] float timeToDisappear = 1f;
    [SerializeField] float attackRange = 1f;

    // setup
    [SerializeField] GameObject player;
    // HACK !!!DO NOT change the order of the box colliders!!!
    [SerializeField] Collider attackCollider; 

    // private fields
    private enum AIState
    {
        chasingPlayer
    }
    private AIState aiState;
    private Animator animator;
    private NavMeshAgent agent;
    private VelocityReporter velocityReporter;
    private float timer;
    private bool attackAnimationFinished = true;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        velocityReporter = player.GetComponent<VelocityReporter>();
        attackCollider = GetComponent<BoxCollider>();
        aiState = AIState.chasingPlayer;
    }

    void Update()
    {
        timer += Time.deltaTime;
        StateMachineBehavior();        
    }

    private void StateMachineBehavior()
    {
        switch (aiState)
        {
            case AIState.chasingPlayer:
                chasingPlayer();
                death();
                break;                         
        }
    }

    private void chasingPlayer()
    {
        animator.SetBool("Walk", true);
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 playerVelocity = velocityReporter.velocity;
        float lookaheadTime = Mathf.Clamp(playerDistance / agent.speed, 0, 1.0f);
        Vector3 extrapolatedPosition = player.transform.position + (playerVelocity * lookaheadTime);
        agent.SetDestination(extrapolatedPosition);

        if (playerDistance <= attackRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (attackAnimationFinished)
        {
            attackAnimationFinished = false;
            animator.SetTrigger("Attack");
        }
    }

    private void death()
    {
        if (timer >= deathCountDown)
        {
            animator.SetTrigger("Death");
            agent.isStopped = true;
            Destroy(gameObject, timeToDisappear);
        }
    }

    public void setTarget(GameObject target)
    {
        player = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerScript script = other.gameObject.GetComponent<PlayerScript>();
            script.death("You were sent to your maker");
        }
    }

    // Animation event
    public void AttackColliderActivate()
    {
        attackCollider.enabled = true;
    }

    // Animation event
    public void AttackColliderDeactivate()
    {
        attackCollider.enabled = false;
    }

    // Animation event
    public void AttackAnimationFinished()
    {
        attackAnimationFinished = true;
    }
}
