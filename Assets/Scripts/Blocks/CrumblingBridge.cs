using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingBridge : MonoBehaviour
{
    private Animator animator;

    //HACK hard coded player jumppower
    [SerializeField] private float preDefinedJumpPower = 3.3f;

    [SerializeField] private float boostJumpPower = 3.5f;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript script = c.gameObject.GetComponent<PlayerScript>();
            script.jumpPower = boostJumpPower;
            animator.SetBool("isStepped", true);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript script = c.gameObject.GetComponent<PlayerScript>();
            script.jumpPower = preDefinedJumpPower;
        }
    }
}
