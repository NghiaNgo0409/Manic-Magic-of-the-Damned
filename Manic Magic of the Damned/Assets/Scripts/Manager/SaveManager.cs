using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] int code;
    [SerializeField] string id;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Object")
        {
            if(PlayerPrefs.GetInt("CrateBroken" + code, 0) == 1)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            if(PlayerPrefs.GetInt(name + "Dead", 0) == 1)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCrate()
    {
        PlayerPrefs.SetInt("CrateBroken" + code, 1);
    }

    public void SaveEnemy()
    {
        PlayerPrefs.SetInt(id + "Dead", 1);
    }
}
