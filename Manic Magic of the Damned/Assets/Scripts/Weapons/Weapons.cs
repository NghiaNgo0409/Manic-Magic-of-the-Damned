using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Sword,
    Bow,
    Spear,
}

public class Weapons : MonoBehaviour
{
    [SerializeField] WeaponTypes types;
    public WeaponTypes Types
    {
        get { return types; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(types == WeaponTypes.Sword)
        {
            if(PlayerPrefs.GetInt("SwordPicked",0) == 1)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else if(types == WeaponTypes.Bow)
        {
            if(PlayerPrefs.GetInt("BowPicked",0) == 1)
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
}
