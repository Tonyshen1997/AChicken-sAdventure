using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatapultSystemController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public Transform camPos;
    [SerializeField] DemoCamera cameraController;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Raise()
    {
        animator.SetBool("IsRaised", true);
    }

    public void ReturnCameraToPlayer()
    {
        cameraController.MoveToPlayer();
    }

}
