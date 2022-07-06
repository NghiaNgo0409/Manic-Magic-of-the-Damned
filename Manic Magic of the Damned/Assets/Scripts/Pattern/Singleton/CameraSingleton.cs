using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSingleton : MonoBehaviour
{
    public static CameraSingleton instance;
    [SerializeField] CinemachineConfiner confiner;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!confiner.m_BoundingShape2D)
        {
            confiner.m_BoundingShape2D = GameObject.Find("BG").GetComponent<PolygonCollider2D>();
        }
    }
}
