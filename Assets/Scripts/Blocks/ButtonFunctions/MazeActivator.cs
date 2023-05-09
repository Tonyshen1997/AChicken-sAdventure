using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeActivator : MonoBehaviour
{
    [SerializeField] GameObject maze;
    private Animator animator;

    private void Start()
    {
        animator = maze.GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            animator.SetTrigger("Activate");
        }

    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            animator.ResetTrigger("Activate");
        }
    }
}
