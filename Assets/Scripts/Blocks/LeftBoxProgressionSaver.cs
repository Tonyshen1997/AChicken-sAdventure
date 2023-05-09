using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoxProgressionSaver : MonoBehaviour
{
    private bool curState;

    private void Awake()
    {
        curState = Convert.ToBoolean(PlayerPrefs.GetInt("LeftBox", 0));
        //Debug.Log(curState);
        if (curState == false)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
            gameObject.GetComponentInChildren<Rigidbody>().useGravity = false;
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
            gameObject.GetComponentInChildren<Rigidbody>().useGravity = true;
        }
    }

    private void Update()
    {
        if (gameObject.GetComponentInChildren<MeshRenderer>().enabled)
        {
            PlayerPrefs.SetInt("LeftBox", 1);
            curState = true;
        }
        //Debug.Log(PlayerPrefs.GetInt("leftBox", 0));
    }
}
