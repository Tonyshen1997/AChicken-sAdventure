using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSwim : MonoBehaviour
{

    public List<GameObject> waterWaypoints;

    
    public float speed=1.5f;
    int index=0;
    public bool isRepeat=true;
    public float turnSpeed = 10.0f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 des=waterWaypoints[index].transform.position;
        Vector3 newDes=Vector3.MoveTowards(transform.position,des,speed*Time.deltaTime);
        
        transform.position=newDes;
        
        // Calculate angel
        //Vector3 targetDir = waterWaypoints[index].transform.position - transform.position;
        
        transform.LookAt(2 * transform.position - waterWaypoints[index].transform.position);
        //Debug.Log("Lookat:"+waterWaypoints[index].transform.position);
        //float angle = Vector3.Angle(targetDir, transform.forward);
        
        //if (angle > 1.0f) {
        //    Quaternion targetRotation = Quaternion.LookRotation(transform.position-targetDir);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //    //Debug.Log("turn");
        //}
        float distance=Vector3.Distance(transform.position,des);
        if(distance<=0.04){
            if(index<waterWaypoints.Count-1){
                
                index+=1;
            }else{
                if(isRepeat){
                    index=0;
                }
            }
            
            
        }

       
        
    }
}
