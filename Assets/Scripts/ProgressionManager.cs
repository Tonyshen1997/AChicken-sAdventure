using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgressionManager : MonoBehaviour
{
    private static ProgressionManager _instance = null;
    private bool RightBridgeActivatorState = false;
    [SerializeField] GameObject RightBridgeActivator;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            if (Convert.ToBoolean(PlayerPrefs.GetInt("RightBridgeActivator", 0)))
            {
                RightBridgeActivator.SetActive(true);
            }
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlayerPrefs.DeleteAll();
        }
    }

    //private void Update()
    //{
    //    RightBridgeActivatorState = Convert.ToBoolean(PlayerPrefs.GetInt("RightBridgeActivator", 0));
    //    if (RightBridgeActivatorState == true)
    //    {
    //        RightBridgeActivator.SetActive(true);
    //        return;
    //    }

    //    if (RightBridgeActivator.activeSelf)
    //    {
    //        PlayerPrefs.SetInt("RightBridgeActivator", 1);
    //    }
    //}
}
