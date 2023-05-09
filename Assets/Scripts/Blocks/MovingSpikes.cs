using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSpikes : MonoBehaviour
{

    private Animator anim;
    [SerializeField] float offsetTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("MovingSpikes", -1, offsetTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
