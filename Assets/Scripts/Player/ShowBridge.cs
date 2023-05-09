using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBridge : MonoBehaviour
{
    public Renderer bridgeRender;
    //private int length=0;
    public AudioClip soundClip; 
    public float volume = 1f; 

    private AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        
            bridgeRender.enabled=false;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = soundClip;
            audioSource.volume = volume;
            audioSource.Stop();

    }

    //can't enable the bridge to show
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            bridgeRender.enabled=true;
            audioSource.Play();
            Debug.Log("Red Button Sound");
            
        }
            
    }
   private void OnTriggerExit(Collider c)
    {
        
        audioSource.Stop();
    }
    
}
