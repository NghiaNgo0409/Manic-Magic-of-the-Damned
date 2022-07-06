using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] string scenePassword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.sceneNewPassword = scenePassword;
            PlayerPrefs.SetInt("Level" + 1, 1);
            GameManager.instance.menu.OpenCanvas("Win");
            PlayerController.instance.state = State.Win;
            PlayerPrefs.SetFloat("HealthKey", GameManager.instance.health.CurrentHealth);
            GameManager.instance.soundManager.PlaySFX(2);
        }    
    }
}
