using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGroups : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript script = c.gameObject.GetComponent<PlayerScript>();
            script.setHidden(true);
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript script = c.gameObject.GetComponent<PlayerScript>();
            script.setHidden(false);
        }
    }
}
