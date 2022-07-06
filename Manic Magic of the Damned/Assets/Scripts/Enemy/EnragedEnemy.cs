using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnragedEnemy : EnemyController
{
    [SerializeField] GameObject impactEffect;

    [SerializeField] Transform attackPos;

    [SerializeField] float attackRange;
    [SerializeField] int damage;

    bool isFacingRight = true;
    
    [SerializeField] LayerMask playerLayer;
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

    public override void LookAtPlayer()
    {
        if(PlayerController.instance.transform.position.x < transform.position.x && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180f, 0);
        }
        else if(PlayerController.instance.transform.position.x > transform.position.x && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180f, 0);
        }
    }

    public void Attack()
    {
        var hit = Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer);

        if(hit)
        {
            hit.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
            var impact = SpawnImpact(hit.transform.position);
            Destroy(impact, .6f);
        }
    }

    GameObject SpawnImpact(Vector3 position)
    {
        return Instantiate(impactEffect, position, transform.rotation);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
