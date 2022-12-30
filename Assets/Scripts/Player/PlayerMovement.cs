using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region VARIABLES
    [Header("COMPONENT")]
    private Rigidbody2D RB;


    [Header("GRAVITY")]
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityMultiplier;


    [Header("RUN")]
    [SerializeField] private float runMaxSpeed;
    [SerializeField] private float runAcceleration;
    [SerializeField] private float runDecceleration;
    private float _moveInput;
    // private float _touchMoveInput;


    [Header("JUMP")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float amountOfJump;
    [SerializeField] private GameObject jumpParticle;
    private float amountOfJumpsLeft;


    [Header("CLIMBING")]
    [SerializeField] private float speed = 8f;
    private float vertical;
    private bool isLadder;
    private bool isClimbing;


    // [Header("WALL SLIDE")]
    // [SerializeField] private float wallSlidingSpeed;


    // [Header("WALL JUMP")]
    // [SerializeField] private Vector2 wallJumpingPower = new Vector2(12f, 40f);
    // private float wallJumpingDuration = 0.3f;
    // private float wallJumpingDirection;
    // private float lastWallJumpDirection;


    [Header("STATUS")]
    private bool isFacingRight;
    private bool isGrounded;
    // private bool isTouchingWall;
    private bool isJumping;
    // private bool isWallSliding;
    // private bool isWallJumping;
    private int facingDirection;


    [Header("TIMERS")]
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    [SerializeField] private float JumpBufferTime = 0.1f;
    private float JumpBufferCounter;
    // [SerializeField] private float wallJumpingTime = 0.1f;
    // private float wallJumpingCounter;


    [Header("COLLISION CHECK")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Vector2 groundCheckSize;
    // [SerializeField] private Transform wallCheck;
    // [SerializeField] private LayerMask wallLayer;
    // [SerializeField] private float wallCheckDistance;
    #endregion

    Player player;
    Animator anim;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("Ground Check");
        // wallCheck = transform.Find("Wall Check");
        player = FindObjectOfType<Player>();
        isFacingRight = true;
        facingDirection = 1;
    }

    void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            JumpBufferCounter = JumpBufferTime;
        } else
        {
            JumpBufferCounter -= Time.deltaTime;
        }

        if(coyoteTimeCounter > 0f && JumpBufferCounter > 0f && !isJumping)
        {   
            Jump();

            StartCoroutine(JumpCooldown());
        }

        if(Input.GetKeyUp(KeyCode.Space) && RB.velocity.y > 0f)
        {
            RB.AddForce(Vector2.down * RB.velocity.y * (1 - 0.5f), ForceMode2D.Impulse);
            amountOfJumpsLeft--;
        }        


        if(RB.velocity.y < 0f)
        {
            RB.gravityScale = gravityScale * fallGravityMultiplier;
        } else {
            RB.gravityScale = gravityScale;
        }


        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
        
        CheckCollision();
        // CheckJump();
    }

    void FixedUpdate()
    {
        Run();
        Climbing();
    }

    private void Run()
    {
        CheckIfShouldFlip();

        float targetSpeed = _moveInput * runMaxSpeed;
        float speedDif = targetSpeed - RB.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAcceleration : runDecceleration;
        float movement = speedDif * accelRate;

        RB.AddForce(movement * Vector2.right);

        if(_moveInput == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(RB.velocity.x), 0.2f);
            amount *= Mathf.Sign(RB.velocity.x);

            RB.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

    }

    void Climbing()
    {
        if(isClimbing)
        {
            RB.gravityScale = 0f;
            RB.velocity = new Vector2(RB.velocity.x, vertical * speed);
        }
        else
        {
            RB.gravityScale = gravityScale;
        }
    }


    // private void CheckJump()
    // {
        
    //     if(isTouchingWall && !isGrounded && _moveInput != 0f)
    //     {
    //         WallSlide();
    //         isWallSliding = true;
            
    //     } else 
    //     {
    //         isWallSliding = false;
    //     }

    //     if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0)
    //     {
    //         if(!isTouchingWall)
    //         {
    //             wallJumpingDirection = facingDirection;
    //         }
    //         WallJump();
    //     }

    // }

    private void Jump()
    {
        coyoteTimeCounter = 0f;
        JumpBufferCounter = 0f;

        RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        anim.SetTrigger("Jump");
        GameObject particle = Instantiate(jumpParticle, transform.position, Quaternion.identity);
        particle.transform.Rotate(90,0,0);

        FindObjectOfType<SoundManager>().Play("JumpWhoosh");
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.3f);
        isJumping = false;
    }

    // private void WallSlide()
    // {
    //     RB.velocity = new Vector2(RB.velocity.x, Mathf.Clamp(RB.velocity.y, -wallSlidingSpeed, float.MaxValue));
    // }

    // private void WallJump()
    // {
    //     wallJumpingCounter = 0f;
    //     isWallJumping = true;
    //     // canWalljump = false;
    //     if(wallJumpingDirection != facingDirection){Flip();}

    //     RB.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

    //     Invoke(nameof(StopWallJumping),wallJumpingDuration);
    // }

    // private void StopWallJumping()
    // {
    //     isWallJumping = false;
    // }

    private void CheckCollision()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        // isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, groundLayer);


        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            amountOfJumpsLeft = amountOfJump;
        } else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // if(isWallSliding)
        // {
        //     wallJumpingCounter = wallJumpingTime;
        //     wallJumpingDirection = -facingDirection;
        // } else 
        // {
        //     wallJumpingCounter -= Time.deltaTime;
        // }
    }

    private void CheckIfShouldFlip()
    {
        if(_moveInput > 0 && !isFacingRight)
        {
            Flip();
        } else if(_moveInput < 0 && isFacingRight) 
        {
            Flip();
        }
    }    

    private void Flip()
    {
        facingDirection *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180.0f, 0f);
    }

    public void Knockback(Vector2 knockback)
    {
        if(player.isInvulnerable)
        {
            return;
        }
        RB.AddForce(new Vector2(knockback.x, knockback.y), ForceMode2D.Impulse);
    }

    void OnEnable()
    {
        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        // Gizmos.color = Color.blue;
        // Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

}