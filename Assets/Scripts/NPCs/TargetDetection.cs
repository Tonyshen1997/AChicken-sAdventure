using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [SerializeField] float timeDuration = 0f;
    private float timer;
    private bool objectInside = false;
    private PlayerScript playerScript;
    private string deathMessage = "You were sent to your maker";

    private void Start()
    {
        timer = timeDuration;    
    }

    private void Update()
    {
        if (objectInside)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                playerScript.death(deathMessage);
                objectInside = false;
            }
        }
    }

    private void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            Enemy_AI parent = transform.parent.gameObject.GetComponent<Enemy_AI>();
            parent.SetAttack(true);
            objectInside = true;
            playerScript = c.gameObject.GetComponent<PlayerScript>();
        }
    }


    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            Enemy_AI parent = this.transform.parent.gameObject.GetComponent<Enemy_AI>();
            parent.SetAttack(false);
            objectInside = false;
            timer = timeDuration;
        }
    }
}
