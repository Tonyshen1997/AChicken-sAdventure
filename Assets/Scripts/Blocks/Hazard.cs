using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    [SerializeField] string deathMessage;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript player = c.gameObject.GetComponent<PlayerScript>();
            player.death(deathMessage);
        }
    }
}
