using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSideQuestFinalButton : MonoBehaviour
{
    [SerializeField] GameObject teleportTarget;
    [SerializeField] GameObject movableBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = teleportTarget.transform.position;
            movableBox.GetComponentInChildren<MeshRenderer>().enabled = true;
            movableBox.GetComponentInChildren<BoxCollider>().enabled = true;
            movableBox.GetComponentInChildren<Rigidbody>().useGravity = true;
        }
    }
}
