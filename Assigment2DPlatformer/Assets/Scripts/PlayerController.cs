using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    Animator playerAnim;
    
    float movementInput;
    float jumpTimer;
    float turnTimer;
    float wallJumpTimer;
    
    int amountOfJumpsLeft;
    int facingDirection = 1;
    int lastWallJumpDirection; 
    
    [SerializeField] float movementSpeed;
    [SerializeField] float wallSlideSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airDragMultiplier = 0.95f;
    [SerializeField] float variableAirJump = 0.5f;
    [SerializeField] float groundRadius;
    [SerializeField] float wallDistance;
    [SerializeField] int amountOfJumps = 1;
    [SerializeField] float wallJumpForce;
    [SerializeField] float jumpTimerSet = 0.15f;
    [SerializeField] float turnTimerSet = 0.1f;
    [SerializeField] float wallJumpTimerSet = 0.5f;

    [SerializeField] float ledgeClimbXOffSet1;
    [SerializeField] float ledgeClimbYOffSet1;
    [SerializeField] float ledgeClimbXOffSet2;
    [SerializeField] float ledgeClimbYOffSet2;

    
    bool isFacingRight = true;
    bool isWalking;
    bool isOnGround;
    bool isTouchingWall;
    bool canNormalJump;
    bool canWallJump;
    bool isWallSliding;
    bool isAttemptingToJump;
    bool checkJumpMultiplier;
    bool canMove;
    bool canFlip;
    bool hasWallJumped;
    bool isTouchingLedge;
    bool canClimbLedge = false;
    bool ledgeDetected;
    
    Vector2 ledgePosBot;
    Vector2 ledgePos1;
    Vector2 ledgePos2;

    [SerializeField] Vector2 wallJumpDirection;
    [SerializeField] Transform groundPos;
    [SerializeField] Transform wallPos;
    [SerializeField] Transform ledgePos;
    [SerializeField] LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckDirectionMovement();
        UpdateAnimations();
        CheckCanJump();
        CheckCanWallSlide();
        CheckJump();
        CheckLedgeClimb();
    }
    
    private void FixedUpdate() {
        Move();
        CheckSurroundings();
    }

    void CheckInput()
    {
        movementInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isOnGround || (amountOfJumpsLeft > 0 && !isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }
        if(Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if(!isOnGround && movementInput != facingDirection)
            {
                canFlip = false;
                canMove = false;

                turnTimer = turnTimerSet;
            }
        }
        if(turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if(turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }
        if(checkJumpMultiplier && !Input.GetKey(KeyCode.Space))
        {
            checkJumpMultiplier = false;
            playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * variableAirJump);
        }
    }

    void CheckDirectionMovement()
    {
        if(isFacingRight && movementInput < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInput > 0)
        {
            Flip();
        }

        if(movementInput != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
    }

    void CheckSurroundings()
    {
        isOnGround = Physics2D.OverlapCircle(groundPos.position, groundRadius, groundLayer);
        isTouchingWall = Physics2D.Raycast(wallPos.position, transform.right * facingDirection, wallDistance, groundLayer);
        isTouchingLedge = Physics2D.Raycast(ledgePos.position, transform.right * facingDirection, wallDistance, groundLayer);

        if(isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallPos.position;
        }
    }

    void CheckCanJump()
    {
        if(isOnGround && playerRb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if(isTouchingWall)
        {
            canWallJump = true;
        }
        
        if(amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    void CheckCanWallSlide()
    {
        if(isTouchingWall && movementInput == facingDirection && playerRb.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    void CheckLedgeClimb()
    {
        if(ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if(isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallDistance) - ledgeClimbXOffSet1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffSet1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallDistance) + ledgeClimbXOffSet2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffSet2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallDistance) + ledgeClimbXOffSet1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffSet1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallDistance) - ledgeClimbXOffSet2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffSet2);
            }
            
            canMove = false;
            canFlip = false;
        }

        if(canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    void UpdateAnimations()
    {
        playerAnim.SetBool("isWalking", isWalking);
        playerAnim.SetBool("isGrounded", isOnGround);
        playerAnim.SetFloat("yVelocity", playerRb.velocity.y);
        playerAnim.SetBool("isWallSliding", isWallSliding);
        playerAnim.SetBool("canClimbLedge", canClimbLedge);
    }

    void Move()
    {
        if(!isOnGround && !isWallSliding && movementInput == 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x * airDragMultiplier, playerRb.velocity.y);
        }
        else if(canMove)
        {
            playerRb.velocity = new Vector2(movementInput * movementSpeed, playerRb.velocity.y);
        }
        
        
        if(isWallSliding)
        {
            if(playerRb.velocity.y < -wallSlideSpeed)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    void Flip()
    {
        if(!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isFacingRight = !isFacingRight;
        }
    }

    void CheckJump()
    {
        if(jumpTimer > 0)
        {
            //wall jump
            if(!isOnGround && isTouchingWall && movementInput != 0 && movementInput != facingDirection)
            {
                WallJump();
            }
            else if(isOnGround)
            {
                NormalJump();
            }
        }
        
        if(isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }
        
        if(wallJumpTimer > 0)
        {
            if(hasWallJumped && movementInput == -lastWallJumpDirection)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0.0f);
            }
            else if(wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }
    
    void NormalJump()
    {
        if(canNormalJump)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    void WallJump()
    {
        if(canWallJump)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpDirection.x * wallJumpForce * movementInput, wallJumpDirection.y * wallJumpForce);
            playerRb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canFlip = true;
            canMove = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundPos.position, groundRadius);
        Gizmos.DrawLine(wallPos.position, new Vector3(wallPos.position.x + wallDistance, wallPos.position.y, wallPos.position.z));
        Gizmos.DrawLine(ledgePos.position, new Vector3(ledgePos.position.x + wallDistance, ledgePos.position.y, ledgePos.position.z));
    }
}
