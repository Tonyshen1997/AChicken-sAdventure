using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    private int curState = 0;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Movable" || c.gameObject.tag == "Player")
        {
            curState = 1;
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            curState = 0;
        }
    }

    public int getCurState()
    {
        return curState;
    }
}
