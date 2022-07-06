using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : EnemyHealthSystem
{
    [SerializeField] GameObject winDoor;
    [SerializeField] Sprite openDoor;
    // Start is called before the first frame update
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

        if(currentHealth <= 200)
        {
            GetComponent<Animator>().SetTrigger("Enraged");
        }

        if(currentHealth == 0)
        {
            controller.isDead = true;
            winDoor.GetComponent<SpriteRenderer>().sprite = openDoor;
            winDoor.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
