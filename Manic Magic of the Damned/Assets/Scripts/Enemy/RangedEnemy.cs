using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    [SerializeField] float distanceCheck;
    [SerializeField] float attackDuration;
    float attackTimer;
    bool isAttack;
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
        CheckPlayer();
        Attack();
    }

    void CheckPlayer()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.left, distanceCheck, playerLayer);

        if(hit)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }

    void Attack()
    {
        if(isAttack)
        {
            if(attackTimer <= 0)
            {
                anim.SetBool("Attack", true);
                attackTimer = attackDuration;
            }
            else
            {
                attackTimer -= Time.deltaTime;
                anim.SetBool("Attack", false);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, distanceCheck);
    }
}
