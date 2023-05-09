using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewChanger : MonoBehaviour
{
    [SerializeField] DemoCamera cam;
    [SerializeField] float magnification = 1.5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam.ViewChange(magnification);
        }
    }
}
