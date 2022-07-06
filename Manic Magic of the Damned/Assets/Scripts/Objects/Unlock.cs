using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    [SerializeField] BoxCollider2D winCollider;
    [SerializeField] EnemyHealthSystem spiderDenHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spiderDenHealth.CurrentHealth <= 0)
        {
            winCollider.enabled = true;
        }
    }
}
