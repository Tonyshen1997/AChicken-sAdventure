using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] string forceDirection;

    private Vector3 _forceDirection;

    private void Start()
    {
        if (forceDirection != null)
        {
            switch (forceDirection)
            {
                case "x":
                    _forceDirection = new Vector3(force, 0, 0);
                    break;
                case "-x":
                    _forceDirection = new Vector3(-1 * force, 0, 0);
                    break;

                case "z":
                    _forceDirection = new Vector3(0, 0, force);
                    break;

                case "-z":
                    _forceDirection = new Vector3(0, 0, -1 * force);
                    break;
            }
        }

        //Debug.Log("force: " + force + ", force direction: " + _forceDirection);
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript script = c.gameObject.GetComponent<PlayerScript>();
            script.AddImpact(_forceDirection, force);
        }

        //Rigidbody rb = c.GetComponent<Rigidbody>();
        //if (rb != null)
        //{
        //    rb.AddForce(_forceDirection, ForceMode.Force);
        //}
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript script = c.gameObject.GetComponent<PlayerScript>();
            script.AddImpact(_forceDirection, 0);
        }
    }
}
