using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoving : MonoBehaviour
{
    private Animator roadAnim;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
     roadAnim=GetComponent<Animator>(); 
    Debug.Log("not move");
     roadAnim.SetBool("isMoving",false);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("move");
        //roadAnim.SetBool("isMoving",true);
    }
}
