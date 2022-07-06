using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    [SerializeField] string scenePassword;

    void Start()
    {
        if(PlayerController.instance.sceneNewPassword == scenePassword)
        {
            PlayerController.instance.transform.position = transform.position;
        }
    }
}
