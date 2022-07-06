using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Base,
    Sword,
    Bow,
}

public class TestEnum : MonoBehaviour
{
    Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = Weapon.Sword;
        Debug.Log(((int)weapon));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
