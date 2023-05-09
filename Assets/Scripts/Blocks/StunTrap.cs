using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            Boss_AI boss = other.gameObject.GetComponent<Boss_AI>();
            boss.stunned = true;
            Destroy(gameObject);
        }
    }
}
