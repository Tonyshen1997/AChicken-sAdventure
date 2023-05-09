using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoad : MonoBehaviour
{
    private int curState = 0;
    public GameObject Road;

    public int MoveSpeed=1;
    Vector3 target;
    void Start(){
        Debug.Log("MoveRoad");
        

    }
    private void Update(){
        Road.transform.Translate(transform.forward*1*1);
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Movable" || c.gameObject.tag == "Player")
        {
            
        
            curState = 1;
            //Debug.Log("detect, entering!" + gameObject.name + "- state:" + curState);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        //if (c.gameObject.tag == "Movable")
        if (c.gameObject.tag == "Player")
        {
            curState = 0;
            //Debug.Log("detect, leaving!" + gameObject.name + "- state:" + curState);
        }
    }

    public int getCurState()
    {
        return curState;
    }
}
