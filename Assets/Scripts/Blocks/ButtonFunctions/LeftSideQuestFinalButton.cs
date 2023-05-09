using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideQuestFinalButton : MonoBehaviour
{

    [SerializeField] GameObject teleportTarget;
    [SerializeField] GameObject movableBox;
    [SerializeField] GameObject rightQuestButton;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = teleportTarget.transform.position;
            movableBox.GetComponentInChildren<MeshRenderer>().enabled = true;
            movableBox.GetComponentInChildren<BoxCollider>().enabled = true;
            movableBox.GetComponentInChildren<Rigidbody>().useGravity = true;
            rightQuestButton.SetActive(true);
            PlayerPrefs.SetInt("RightBridgeActivator", 1);
        }
    }
}
