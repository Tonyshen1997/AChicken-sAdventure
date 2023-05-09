using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDeactivator : MonoBehaviour
{
    [SerializeField] GameObject Spike;
    [SerializeField] bool lowerWhenLeave = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = Spike.transform.Find("Spikes").gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" || c.gameObject.tag == "Movable")
        {
            animator.SetBool("IsRaised", false);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (lowerWhenLeave)
        {
            if (c.gameObject.tag == "Player" || c.gameObject.tag == "Movable")
            {
                animator.SetBool("IsRaised", true);
            }
        }
    }
}
