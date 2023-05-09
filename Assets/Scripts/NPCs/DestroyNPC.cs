using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNPC : MonoBehaviour
{
   
    public Renderer roadRender;
    public AudioClip soundClip; 
    public float volume = 1f; 
    private AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
            roadRender.enabled=false;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = soundClip;
            audioSource.volume = volume;
            audioSource.Stop();
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            // from top
            if (transform.position.y < other.transform.position.y)
            {
                // destroy NPC
                Destroy(gameObject);
                roadRender.enabled=true;
                audioSource.Play();
                Debug.Log("Red NPC Dead Sound");
            }
        }
    }

    private void OnTriggerExit(Collider c)
    {
        
        audioSource.Stop();
    }
}
