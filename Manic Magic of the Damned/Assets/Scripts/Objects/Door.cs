using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    [SerializeField] EnemyTrigger enemyTrigger;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinishOpeningDoor()
    {
        if(gameObject.CompareTag("ThisDoor"))
        {
            Debug.Log("Finish");
            enemyTrigger.TriggerEnemy();
        }
    }
}
