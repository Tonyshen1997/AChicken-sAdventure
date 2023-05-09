using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] Animator Elvator_an;
    [SerializeField] Animator Path_an;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Elvator_an.SetBool("isLower", true);
            Path_an.SetBool("isLower", true);
        }
    }
}
