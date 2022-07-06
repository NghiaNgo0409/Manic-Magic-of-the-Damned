using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    Base,
    Sword,
    Bow,
    Spear
}

public class WeaponManager : MonoBehaviour
{
    public WeaponState weaponState;

    public bool hasSword;
    public bool hasBow;
    public bool hasSpear;
    // Start is called before the first frame update
    void Start()
    {
        weaponState = WeaponState.Base;
        if(PlayerPrefs.GetInt("SwordPicked", 0) == 1)
        {
            hasSword = true;
        }
        if(PlayerPrefs.GetInt("BowPicked", 0) == 1)
        {
            hasBow = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
