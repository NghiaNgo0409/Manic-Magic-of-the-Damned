using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    PlatformEffector2D platform;
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            platform.rotationalOffset = 0f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            platform.rotationalOffset = 180f;
        }
    }
}
