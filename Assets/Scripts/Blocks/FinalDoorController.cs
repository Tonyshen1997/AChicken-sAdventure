using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorController : MonoBehaviour
{
    [SerializeField] GameObject Camera;

    public void MoveCamToPlayer()
    {
        DemoCamera cameraController = Camera.GetComponent<DemoCamera>();
        cameraController.MoveToPlayer();
    }
}
