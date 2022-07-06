using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateArrow : MonoBehaviour
{
    [SerializeField] Transform arrowPos;
    [SerializeField] GameObject arrowPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Instantiate()
    {
        if(transform.name == "Crossbow")
        {
            Instantiate(arrowPrefabs, arrowPos.position, Quaternion.Euler(0, 180, 0));
        }
        else
        {
            Instantiate(arrowPrefabs, arrowPos.position, transform.rotation);
        }
        GameManager.instance.soundManager.PlaySFX(0);
    }
}
