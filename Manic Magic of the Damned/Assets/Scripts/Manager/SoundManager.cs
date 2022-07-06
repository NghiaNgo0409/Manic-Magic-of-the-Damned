using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource[] sfx;
    [SerializeField] AudioSource bgm;
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.RegisterSound(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance)
        {
            if(PlayerController.instance.state == State.Dead) 
            {
                bgm.Stop();
            }
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        sfx[soundToPlay].Stop();
        sfx[soundToPlay].Play();
    }
}
