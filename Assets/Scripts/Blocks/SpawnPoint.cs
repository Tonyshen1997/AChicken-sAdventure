using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        //GameObject[] objs = GameObject.
        //DontDestroyOnLoad(this.gameObject);
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerScript player = c.gameObject.GetComponent<PlayerScript>();
            player.setSpawn(gameObject);
        }
    }
}
