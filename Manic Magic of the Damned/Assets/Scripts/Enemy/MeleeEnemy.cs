using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MeleeEnemy : EnemyController
{
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    [SerializeField] Transform attackZone;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float range;
    [SerializeField] float attackZoneRange;
    [SerializeField] float movementSpeed;
    [SerializeField] float idleDuration;
    float idleTimer;


    [SerializeField] int damage;
    int nextID;
    int idChangeValue = 1;

    bool isFacingRight = true;
    public bool isPatrolling = true;

    [SerializeField] LayerMask playerMask;

    [SerializeField] EnemyHealthSystem enemyHealth;
    PlayerHealthSystem playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        Patrolling();
        if(PlayerInAttackZone())
        {
            if(PlayerInSight() || enemyHealth.isHit)
            {
                isPatrolling = false;
                anim.SetBool("isChasing", true);
            }
            else
            {
                isPatrolling = true;
                anim.SetBool("isChasing", false);
            }
        }
        else
        {
            anim.SetBool("isChasing", false);
            isPatrolling = true;
        }
        
        Debug.DrawRay(transform.position, transform.right * range, Color.cyan);
    }

    protected override void Die()
    {
        base.Die();
        isDead = false;
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }

    public override void LookAtPlayer()
    {
        base.LookAtPlayer();
        if(PlayerController.instance.transform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if(PlayerController.instance.transform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    public bool PlayerInAttackZone()
    {
        var isPlayerInZone = Physics2D.OverlapCircle(attackZone.position, attackZoneRange, playerMask);
        return isPlayerInZone;
    }

    bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, range, playerMask);
        if(hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<PlayerHealthSystem>();
        }
        return hit.collider;
    }

    void MeeleeAttack()
    {
        playerHealth.TakeDamage(damage);
        var impact = SpawnImpact(playerHealth.transform.position);
        Destroy(impact, .6f);
    }

    GameObject SpawnImpact(Vector3 position)
    {
        return Instantiate(impactEffect, position, transform.rotation);
    }

    void Patrolling()
    {
        if(!isPatrolling) return;
        //Assign goalPoint
        Transform goalPoint = waypoints[nextID];

        //Check the direction to flip the enemy
        if(transform.position.x > goalPoint.position.x && isFacingRight)
        {
            Flip();
        }
        else if(transform.position.x < goalPoint.position.x && !isFacingRight)
        {
            Flip();
        }

        //Move the enemy to the goalPoint
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, movementSpeed * Time.deltaTime);

        //Check whether the player is reach goalPoint, enter the "Idle Time", calculate next goalPoint
        if(Vector2.Distance(transform.position, goalPoint.position) == 0)
        {
            anim.SetFloat("Speed", 0);
            if(idleTimer >= idleDuration)
            {
                //If it is the last point now, reverse the journey
                if(nextID == waypoints.Count - 1)
                {
                    idChangeValue = -1;
                }
                else
                {
                    idChangeValue = 1;
                }
                nextID += idChangeValue;
                idleTimer = 0;
            }
            else
            {
                idleTimer += Time.deltaTime;
            }
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackZone.position, attackZoneRange);
    }

}
