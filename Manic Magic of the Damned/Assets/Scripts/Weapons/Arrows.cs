using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    Rigidbody2D arrowRb;

    [SerializeField] ArrowData arrowData;

    [SerializeField] GameObject impactEffect;

    float launchForce;
    // Start is called before the first frame update
    void Start()
    {
        arrowRb = GetComponent<Rigidbody2D>();

        launchForce = arrowData.launchForce;

        arrowRb.velocity = transform.right * launchForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject SpawnImpact(Vector3 position)
    {
        return Instantiate(impactEffect, position, transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.CompareTag("Lever"))
        {
            if(other.CompareTag("Enemy"))
            {
                other.GetComponent<MeleeEnemy>().Flip();
                other.GetComponent<MeleeEnemy>().isPatrolling = false;
                other.GetComponent<EnemyHealthSystem>().isHit = false;
            }
            other.GetComponent<HealthSystem>().TakeDamage(20);
            var impact = SpawnImpact(other.transform.position);
            Destroy(impact, .6f);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Lever"))
        {
            other.GetComponent<Lever>().isSwitched = true;
            Destroy(gameObject);
        }
    }
}
