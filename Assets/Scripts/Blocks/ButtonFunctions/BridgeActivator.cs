using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivator : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] bool lowerWhenLeave = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = bridge.transform.Find("Bridges").gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" || c.gameObject.tag == "Movable")
          
        {
            animator.SetBool("IsRaised", true);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (lowerWhenLeave)
        {
            if (c.gameObject.tag == "Player" || c.gameObject.tag == "Movable")
            {
                animator.SetBool("IsRaised", false);
            }
        }
    }
}
