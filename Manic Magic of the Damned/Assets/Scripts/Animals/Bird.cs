using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Animator anim;
    bool flying = false;
    [SerializeField] Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flying)
        {
            anim.SetBool("Flying", flying);
            transform.Translate(direction * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            flying = true;
        }    
    }
}
