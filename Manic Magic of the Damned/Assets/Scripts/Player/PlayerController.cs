using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State 
{
    Normal,
    Rolling,
    Transition,
    Pause,
    Win,
    Dead,
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody2D playerRb;
    public Animator playerAnim;

    Vector3 rollDir;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] Transform attackSwordCheckPos;
    [SerializeField] Transform attackSpearCheckPos;

    float horizontalInput;
    float verticalInput;
    float initialGravityScale;
    float runningSpeed;
    float rollingSpeed;
    float currentRollingSpeed;
    float climbingSpeed;
    float jumpForce;
    [SerializeField] float groundRadius;
    [SerializeField] float attackSwordRadius;
    [SerializeField] float attackSpearRadius;
    [SerializeField] float ladderCheckDistance;

    bool isFacingRight = true;
    bool isOnGround;
    bool isClimbing;
    public bool isAttacking;

    public string sceneNewPassword;

    RaycastHit2D hitLadder;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask ladderLayer;
    [SerializeField] LayerMask enemiesLayer;

    public State state;

    public WeaponManager weaponManager;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject impactEffect;
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
        state = State.Normal;
        initialGravityScale = playerRb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Normal:
                GetInput();
                GetData();
                UpdateAnimator();
                HandleRolling();
                break;
            case State.Rolling:
                Roll();
                break;
            case State.Transition:
                playerRb.velocity = Vector2.zero;
                break;
            case State.Pause:
            case State.Win:
                playerAnim.SetFloat("xVelocity", 0);
                break;
            case State.Dead:
                StartCoroutine(Die());
                break;
        }
        Debug.DrawRay(transform.position, Vector3.up * ladderCheckDistance, Color.cyan);
    }

    void FixedUpdate() 
    {
        switch(state)
        {
            case State.Normal:
                Run();
                CheckCollision();
                Climb();
                break;
            case State.Rolling:
                break;
            case State.Transition:
                playerRb.velocity = Vector2.zero;
                break;
            case State.Pause:
            case State.Win:
                playerRb.velocity = Vector2.zero;
                break;
            case State.Dead:
                break;
        }
    }

    void GetData()
    {
        runningSpeed = playerData.runningSpeed;
        rollingSpeed = playerData.rollingSpeed;
        climbingSpeed = playerData.climbingSpeed;
        jumpForce = playerData.jumpForce;
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeWeapon();
        }

        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            if(isClimbing || !isOnGround) return;
            isAttacking = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.menu.OpenCanvas("Pause");
            state = State.Pause;
        }
    }

    void UpdateAnimator()
    {
        playerAnim.SetFloat("xVelocity", Mathf.Abs(horizontalInput));
        playerAnim.SetFloat("yVelocity", playerRb.velocity.y);
        playerAnim.SetBool("isOnGround", isOnGround);
        playerAnim.SetBool("isClimbing", isClimbing);
    }

    void CheckCollision()
    {
        //Check Ground
        var hitGround = Physics2D.OverlapCircle(groundCheckPos.position, groundRadius, groundLayer);

        if(hitGround) isOnGround = true;
        else isOnGround = false;

        //Check Ladder
        hitLadder = Physics2D.Raycast(transform.position, Vector2.up, ladderCheckDistance, ladderLayer);
        if(hitLadder)
        {
            isClimbing = true;
        }
        else
        {
            if(horizontalInput != 0) isClimbing = false;
        }
    }

    void HandleRolling()
    {
        if(Input.GetMouseButtonDown(1))
        {
            state = State.Rolling;
            currentRollingSpeed = rollingSpeed;
            rollDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized; //Get position of mouse
            playerAnim.SetTrigger("isRolling");
        }
    }

    void Run()
    {
        playerRb.velocity = new Vector2(horizontalInput * runningSpeed * Time.fixedDeltaTime, playerRb.velocity.y);
    }
    
    void Roll()
    {
        float rollingSpeedDropMultiplier = 2f;
        float rollingSpeedMinimun = 3.5f;

        if(rollDir.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if(rollDir.x > 0 && !isFacingRight)
        {
            Flip();
        }

        transform .position += new Vector3(currentRollingSpeed * rollDir.x * Time.deltaTime, 0, 0);
        currentRollingSpeed -= currentRollingSpeed * rollingSpeedDropMultiplier * Time.deltaTime;
        if(currentRollingSpeed <= rollingSpeedMinimun)
        {
            state = State.Normal;
        }
    }

    void Climb()
    {
        if(isClimbing && hitLadder)
        {
            verticalInput = Input.GetAxisRaw("Vertical");
            playerRb.velocity = new Vector2(playerRb.velocity.x, verticalInput * climbingSpeed * Time.fixedDeltaTime);
            if(!isOnGround) playerAnim.speed = Mathf.Abs(verticalInput);
            playerRb.gravityScale = 0;   
        }
        else
        {
            playerAnim.speed = 1;
            playerRb.gravityScale = initialGravityScale;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }

    void Jump()
    {
        if(isOnGround)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            GameManager.instance.soundManager.PlaySFX(8);
        }
    }

    void ChangeWeapon()
    {
        if(isClimbing) return;
        switch (weaponManager.weaponState)
        {
            case WeaponState.Sword:
                if(weaponManager.hasBow)
                {
                    SwapToBow();
                }
                else if(weaponManager.hasSpear)
                {
                    SwapToSpear();
                }
                else
                {
                    SwapToBase();
                }
                break;
            case WeaponState.Bow:
                if(weaponManager.hasSpear)
                {
                    SwapToSpear();
                }
                else
                {
                    SwapToBase();
                }
                break;
            case WeaponState.Spear:
                SwapToBase();
                break;
            default:
                if(weaponManager.hasSword)
                {
                    SwapToSword();
                }
                else if(weaponManager.hasBow)
                {
                    SwapToBow();
                }
                else if(weaponManager.hasSpear)
                {
                    SwapToSpear();
                }
                break;
        }
        if(weaponManager.hasBow || weaponManager.hasSpear || weaponManager.hasSword) GameManager.instance.soundManager.PlaySFX(9);
    }

    public void SwapToSword()
    {
        weaponManager.weaponState = WeaponState.Sword;
        playerAnim.SetLayerWeight(((int)WeaponState.Base), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Sword), 1);
        playerAnim.SetLayerWeight(((int)WeaponState.Bow), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Spear), 0);
    }

    public void SwapToBow()
    {
        weaponManager.weaponState = WeaponState.Bow;
        playerAnim.SetLayerWeight(((int)WeaponState.Base), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Sword), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Bow), 1);
        playerAnim.SetLayerWeight(((int)WeaponState.Spear), 0);
    }

    public void SwapToSpear()
    {
        weaponManager.weaponState = WeaponState.Spear;
        playerAnim.SetLayerWeight(((int)WeaponState.Base), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Sword), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Bow), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Spear), 1);
    }

    public void SwapToBase()
    {
        weaponManager.weaponState = WeaponState.Base;
        playerAnim.SetLayerWeight(((int)WeaponState.Base), 1);
        playerAnim.SetLayerWeight(((int)WeaponState.Sword), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Bow), 0);
        playerAnim.SetLayerWeight(((int)WeaponState.Spear), 0);
    }

    void Attack()
    {
        if(weaponManager.weaponState == WeaponState.Sword)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackSwordCheckPos.position, attackSwordRadius, enemiesLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if(!enemy.CompareTag("Lever"))
                {
                    if(enemy.CompareTag("Enemy"))
                    {

                        enemy.GetComponent<MeleeEnemy>().Flip();
                        enemy.GetComponent<MeleeEnemy>().isPatrolling = false;
                    }
                    enemy.GetComponent<EnemyHealthSystem>().TakeDamage(50);
                    enemy.GetComponent<EnemyHealthSystem>().isHit = false;
                    var impact = SpawnImpact(enemy.transform.position);
                    Destroy(impact, .6f);
                }
                else
                {
                    enemy.GetComponent<Lever>().isSwitched = true;
                }
            }
        }
        else if(weaponManager.weaponState == WeaponState.Spear)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackSpearCheckPos.position, attackSpearRadius, enemiesLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if(!enemy.CompareTag("Lever"))
                {
                    if(enemy.CompareTag("Enemy"))
                    {
                        enemy.GetComponent<MeleeEnemy>().Flip();
                    }
                    enemy.GetComponent<EnemyHealthSystem>().TakeDamage(50);
                    enemy.GetComponent<EnemyHealthSystem>().isHit = false;
                    var impact = SpawnImpact(enemy.transform.position);
                    Destroy(impact, .6f);
                }
                else
                {
                    enemy.GetComponent<Lever>().isSwitched = true;
                }
            }
        }
    }

    GameObject SpawnImpact(Vector3 position)
    {
        return Instantiate(impactEffect, position, transform.rotation);
    }

    void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(groundCheckPos.position, groundRadius);
        Gizmos.DrawWireSphere(attackSwordCheckPos.position, attackSwordRadius);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Weapon"))
        {
            GameManager.instance.soundManager.PlaySFX(7);
            WeaponTypes types = other.GetComponent<Weapons>().Types;
            if(types == WeaponTypes.Sword)
            {
                weaponManager.hasSword = true;
                PlayerPrefs.SetInt("SwordPicked", 1);
            }
            else if(types == WeaponTypes.Bow)
            {
                weaponManager.hasBow = true;
                PlayerPrefs.SetInt("BowPicked", 1);
            }
            else
            {
                weaponManager.hasSpear = true;
            }
            Destroy(other.gameObject);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.RestartLevel();
    }
}
