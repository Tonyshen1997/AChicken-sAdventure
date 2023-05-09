using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFinalButtonController : MonoBehaviour
{
    [SerializeField] GameObject redButton1;
    [SerializeField] GameObject redButton2;
    [SerializeField] GameObject redButton3;
    [SerializeField] GameObject finaleDoor;

    private RedButton script1;
    private RedButton script2;
    private RedButton script3;
    private Animator animator;

    private void Start()
    {
        animator = finaleDoor.GetComponent<Animator>();
        script1 = redButton1.GetComponent<RedButton>();
        script2 = redButton2.GetComponent<RedButton>();
        script3 = redButton3.GetComponent<RedButton>();
    }


    private void Update()
    {
        int curState1 = script1.getCurState();
        int curState2 = script2.getCurState();
        int curState3 = script3.getCurState();

        if (curState1 + curState2 + curState3 == 3)
        {
            animator.SetBool("IsOpen", true);
            Debug.Log("activated");
        }

    }
}
