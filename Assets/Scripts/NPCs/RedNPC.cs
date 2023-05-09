using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedNPC : MonoBehaviour
{
    public List<GameObject> walkPoints;
    int WalkIndex;
    private Transform target;
    private NavMeshAgent navAgent;

    private Animator anim;
    //public int currWaypoint;
    
    // Start is called before the first frame update
    void Start()
    {
        navAgent=GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();
        setNextWaypoint();  
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance<0.5f && navAgent.pathStatus==NavMeshPathStatus.PathComplete && navAgent.hasPath){
                setNextWaypoint();  
        }
         
    }
    private void setNextWaypoint(){
        Debug.Log("setNextWaypoint");
        try{
            WalkIndex= UnityEngine.Random.Range(0,41);
            Debug.Log("WalkIndex:"+ WalkIndex);
            navAgent.SetDestination(walkPoints[WalkIndex].transform.position);
        }catch(Exception ex){
            Debug.Log(ex);
            throw ex;
        }
        
    }

}
