using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isSwitched;
    Animator anim;

    [SerializeField] Door door;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSwitched)
        {
            anim.SetBool("Switch", isSwitched);
            door.anim.SetBool("Switch", isSwitched);
        }
        
    }
}