using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Whale_AI : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    private int currWaypoint;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currWaypoint = -1;
        setNextWaypoint();
    }

    private void Update()
    {
        if (agent.remainingDistance <= 0.5 && !agent.pathPending)
        {
            setNextWaypoint();
        }    
    }


    private void setNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        currWaypoint = (currWaypoint + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currWaypoint].transform.position);
    }
}
