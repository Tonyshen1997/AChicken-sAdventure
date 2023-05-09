using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] RedButton button1;
    //[SerializeField] RedButton button2;
    //[SerializeField] RedButton button3;

    public RedButton[] redButtons;


    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;

    private Animator leftDoorAnimator;
    private Animator rightDoorAnimator;

    private void Start()
    {
        leftDoorAnimator = leftDoor.GetComponent<Animator>();
        rightDoorAnimator = rightDoor.GetComponent<Animator>();
    }

    void Update()
    {
        bool open = true;

        for (int i = 0; i < redButtons.Length; i++)
        {
            if (redButtons[i].getCurState() != 1) open = false;
        }

        if (open)
        {
            openDoor();
        } else
        {
            closeDoor();
        }
    }

    private void openDoor()
    {
        if (leftDoorAnimator != null)
        {
            leftDoorAnimator.SetBool("Open", true);
            rightDoorAnimator.SetBool("Open", true);
        }
    }

    private void closeDoor()
    {
        if (leftDoorAnimator != null)
        {

            leftDoorAnimator.SetBool("Open", false);
            rightDoorAnimator.SetBool("Open", false);
        }
    }
}
