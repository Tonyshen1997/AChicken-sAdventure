using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rotate")) 
        {
            transform.Rotate (new Vector3 (30,0, 0));
        }
    }
}
