using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class Boss_AI : MonoBehaviour
{
    // Fine tune needed
    [SerializeField] float spawnInterval = 3f;
    [SerializeField] float chargeDistance = 10f;
    [SerializeField] float chargeDelay = 2f;
    [SerializeField] float chargeSpeed = 10f;
    [SerializeField] float chargeAcceleration = 20f;
    [SerializeField] float chargeCoolDown = 8f;
    [SerializeField] float stunTime = 5f;
    [SerializeField] float lookSpeed;
    [SerializeField] int hp = 3;

    // Setup
    [SerializeField] GameObject hostileMinionPrefab;
    [SerializeField] GameObject evasiveMinionPrefab;
    [SerializeField] Transform spawnLoc;
    [SerializeField] GameObject player;
    //[SerializeField] GameObject CatapultSystem;
    [SerializeField] GameObject FinalGate;
    [SerializeField] DemoCamera cameraController;
    [SerializeField] GameObject[] hpIndicators;
    [SerializeField] Transform FinalDoorCamPos;
    [SerializeField] GameObject Finale;
    

    // private fields
    public enum AIState
    {
        Idle,
        Phase1,
        Phase2
    }
    public AIState aiState = AIState.Phase1;
    private float timer = 0;
    private Animator animator;
    private NavMeshAgent agent;
    private VelocityReporter velocityReporter;
    private bool hasRed;
    public bool stopSpawning = false;
    public bool stunned = false;
    private bool charging = false;
    private bool chargeReady = false;
    private bool stunning = false;
    private bool underAttack = false;
    //private CatapultSystemController csController;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        velocityReporter = player.GetComponent<VelocityReporter>();
        //aiState = AIState.Phase1;
        //csController = CatapultSystem.GetComponent<CatapultSystemController>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer %= 100;
        StateMachineBehaviour();
    }

    private void StateMachineBehaviour()
    {
        switch (aiState)
        {
            case AIState.Idle:
                break;

            case AIState.Phase1:
                Phase1Behavior();
                break;

            case AIState.Phase2:
                Phase2Behavior();
                break;
        }
    }

    private void Phase1Behavior()
    {
        if (!stopSpawning)
        {
            SpawnMinions();
        }
        FollowPlayer();
    }

    private void Phase2Behavior()
    {
        if (timer >= chargeCoolDown)
        {
            timer = 0;
            chargeReady = true;
        }

        //if (underAttack)
        //{
        //    agent.isStopped = true;
        //    StartCoroutine(Defend());
        //    return;
        //}

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!stunned && !charging && (playerDistance >= chargeDistance || !chargeReady))
        {
            ChasingPlayer(playerDistance);
            return;
        }

        if (chargeReady && !stunned)
        {
            agent.isStopped = true;
            chargeReady = false;
            charging = true;
            StartCoroutine(ChargePlayer());
        }
        else if (!stunning && stunned)
        {
            stunning = true;
            StartCoroutine(Stunned());
        }
    }

    private IEnumerator Defend()
    {
        
        animator.SetBool("Defend", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Defend", false);
        agent.isStopped = false;
        underAttack = false;

    }

    private void ChasingPlayer(float playerDistance)
    {
        if (underAttack)
        {
            agent.isStopped = true;
            StartCoroutine(Defend());
            return;
        }

        animator.SetBool("Run Forward", true);
        Vector3 playerVelocity = velocityReporter.velocity;
        float lookaheadTime = Mathf.Clamp(playerDistance / agent.speed, 0, 1.0f);
        Vector3 extrapolatedPostion = player.transform.position + (playerVelocity * lookaheadTime);
        agent.SetDestination(extrapolatedPostion);

    }

    private IEnumerator ChargePlayer()
    {
        
        animator.SetBool("Run Forward", false);
        animator.SetTrigger("Cast Spell");
        
        yield return new WaitForSeconds(chargeDelay);

        float originalSpeed = agent.speed;
        float originalAcc = agent.acceleration;
        agent.speed = chargeSpeed;
        agent.acceleration = chargeAcceleration;
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        agent.speed = originalSpeed;
        agent.acceleration = originalAcc;

        while (agent.remainingDistance >= agent.stoppingDistance)
        {
            yield return null;
        }
        charging = false;
    }

    private IEnumerator Stunned()
    {
        animator.SetTrigger("Take Damage");
        animator.SetBool("Run Forward", false);
        agent.isStopped = true;
        yield return new WaitForSeconds(stunTime);
        stunning = false;
        stunned = false;
        agent.isStopped = false;
    }

    private void FollowPlayer()
    {
        Vector3 playerDir = player.transform.position - gameObject.transform.position;
        playerDir.y = 0;
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.LookRotation(playerDir), lookSpeed * Time.deltaTime);

        float angleDifference = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(playerDir));
        if (angleDifference != 0)
        {
            animator.SetBool("Strafe Right", true);
        } else
        {
            animator.SetBool("Strafe Right", false);
        }
    }

    private void SpawnMinions()
    {
        if (timer >= spawnInterval)
        {
            // spawn only one red with 30% chance of spawning one
            GameObject curMinion = null;
            if (!hasRed && Random.Range(1, 4) == 1)
            {
                curMinion = evasiveMinionPrefab;
                hasRed = true;
            }
            else
            {
                curMinion = hostileMinionPrefab;
            }
            animator.SetTrigger("Jump");
            if (curMinion != null)
            {
                GameObject clone = Instantiate(curMinion, spawnLoc.position, spawnLoc.rotation);
                Minion ai = clone.GetComponent<Minion>();
                ai.setTarget(player);
            }
            timer = 0;
        }
    }

    private void EnterPhase2()
    {
        aiState = AIState.Phase2;
        foreach (GameObject hpIndicator in hpIndicators)
        {
            hpIndicator.SetActive(true);
        }
    }

    public void getHit()
    {
        if (aiState == AIState.Phase1)
        {
            animator.SetTrigger("Take Damage");
            EnterPhase2();
        } else
        {
            if (!stunned)
            {
                underAttack = true;
            } else
            {                
                hp--;
                hpIndicators[hp].SetActive(false);
                animator.SetTrigger("Take Damage");
            }
        }
    }

    private void Death()
    {
        animator.SetTrigger("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerScript playerController = other.gameObject.GetComponent<PlayerScript>();
            playerController.death("Killed by boss");
        }
    }


    // Animation Event
    public void DefendFinished()
    {
        animator.SetBool("Defend", false);
    }


    // Animation Event
    public void CheckDeath()
    {
        if (hp == 0) Death();
    }

    // Animation Event
    public void CorpDisappear()
    {
        FinalGate.GetComponent<Animator>().SetBool("Open", true);
        Finale.GetComponent<Animator>().SetBool("IsRaise", true);
        Destroy(gameObject);
    }

    // Animation Event
    public void MoveCamera()
    {
        cameraController.MoveToPosition(FinalDoorCamPos);
        
    }
}
