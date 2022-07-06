using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnrageEnemy_Run : StateMachineBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;
    float attackTimer;

    PlayerController player;
    Rigidbody2D rb;

    EnemyController enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = PlayerController.instance;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<EnemyController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemy.LookAtPlayer();
       Vector2 target = new Vector2(player.transform.position.x, rb.position.y);
       Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
       

       if(Vector2.Distance(player.transform.position, rb.position) <= attackRange && player.state == State.Normal)
       {
            animator.SetFloat("Speed", 0);
            if(attackTimer <= 0)
            {
                animator.SetTrigger("Attack");
                attackTimer = attackDuration;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
       }
       else if(player.state == State.Dead)
       {
            animator.SetFloat("Speed", 0);
       }
       else
       {
            rb.MovePosition(newPos);
            animator.SetFloat("Speed", 1);
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
    }
}
