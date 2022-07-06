using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Animator anim;
    public bool isDead = false; 
    [SerializeField] protected CoinSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    public virtual void LookAtPlayer()
    {

    }

    protected virtual void Die()
    {
        if(isDead && gameObject.tag != "Spawner")
        {
            anim.SetTrigger("Dead");
            gameObject.GetComponent<Collider2D>().enabled =  false;
            if(gameObject.tag != "Object")
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                GameManager.RemoveEnemy(this);
            }
            spawner.SpawnCoin(transform);
            Destroy(gameObject, 1f);
            this.enabled = false;
        }
        else if(isDead && gameObject.tag == "Spawner")
        {
            gameObject.GetComponent<Collider2D>().enabled =  false;
            spawner.SpawnCoin(transform);
            Destroy(gameObject);
            this.enabled = false;
        }
    }
}
