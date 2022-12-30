using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [Header("PATROLLING")]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;
    private float facingDirection = 1;
    private bool isFacingRight = true;

    [Header("JUMP ATTACK")]
    [SerializeField] private float delayAttackTime;
    private float delayAttackTimeCounter;
    [SerializeField] private float jumpAttackForce;
    [SerializeField] private Vector2 knockbackForce;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private int damage;
    private bool isGrounded;

    [Header("VISION")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    private Rigidbody2D RB;
    private Animator anim;

    Vector2 startPosition;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        startPosition = transform.position;
    }

    void Update()
    {
        delayAttackTimeCounter -= Time.deltaTime;

        CheckSurroundings();
        AnimationController();
    }

    void FixedUpdate()
    {
        if(!canSeePlayer && isGrounded)
        {
            Patrolling();
            delayAttackTimeCounter = delayAttackTime;
        } else if(canSeePlayer && delayAttackTimeCounter <= 0 && isGrounded)
        {
            JumpAttack();
            delayAttackTimeCounter = delayAttackTime;
        }
    }

    void Patrolling()
    {
        if(!checkingGround || checkingWall)
        {
            if(isFacingRight)
            {
                Flip();
            } else if(!isFacingRight)
            {
                Flip();
            }
        }
        RB.velocity = new Vector2(moveSpeed * facingDirection, RB.velocity.y);
    }


    void JumpAttack()
    {

        float distanceFromPlayer = player.position.x - transform.position.x;
        if(distanceFromPlayer < 0)
        {
            distanceFromPlayer -= 5;
        } else
        {
            distanceFromPlayer += 5;
        }

        if(isGrounded)
        {
            RB.AddForce(new Vector2(distanceFromPlayer, jumpAttackForce), ForceMode2D.Impulse);
        }
    }


    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if(playerPosition < 0 && isFacingRight)
        {
            Flip();
        } else if(playerPosition > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        knockbackForce.x *= -1;
        isFacingRight = !isFacingRight;

        transform.Rotate(0,180,0);
    }



    void CheckSurroundings()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);
    }

    void AnimationController()
    {
        if(!canSeePlayer)
        {
            anim.SetBool("isWalking", true);
        } else 
        {
            anim.SetBool("isWalking", false);
        }

        anim.SetBool("isGrounded", isGrounded);
    }

    public void Respawn()
    {
        transform.position = startPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!isGrounded)
            {
                other.GetComponent<PlayerMovement>().Knockback(knockbackForce);
                other.GetComponent<Player>().TakeDamage(damage);            
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
    }
}