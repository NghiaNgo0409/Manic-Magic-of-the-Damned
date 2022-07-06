using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startPos;
    [SerializeField] Camera cam;
    [SerializeField] float parallaxValue;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        if(!cam) cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxValue); //The variable to check for looping parallax
        float dist = cam.transform.position.x * parallaxValue;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if(temp > startPos + length)
        {
            startPos += length;
        }
        else if(temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
