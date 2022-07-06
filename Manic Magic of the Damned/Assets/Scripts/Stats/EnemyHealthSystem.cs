using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] protected EnemyData data;
    [SerializeField] protected EnemyController controller;
    [SerializeField] SaveManager saveManager;

    public bool isHit;

    void Awake() 
    {
        maxHealth = data.health;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if(currentHealth == 0)
        {
            controller.isDead = true;
            if(gameObject.CompareTag("Object"))
            {
                saveManager.SaveCrate();
            }
            else if(gameObject.name == "Skeleton")
            {
                saveManager.SaveEnemy();
            }
        }

        isHit = true;
    }
}
