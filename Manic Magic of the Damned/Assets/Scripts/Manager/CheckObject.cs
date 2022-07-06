using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObject : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance)
        {
            Destroy(PlayerController.instance.gameObject);
        }
        
        if(CameraSingleton.instance)
        {
            Destroy(CameraSingleton.instance.gameObject);
        }

        if(CanvasSingleton.instance)
        {
            Destroy(CanvasSingleton.instance.gameObject);
        }
    }
}
