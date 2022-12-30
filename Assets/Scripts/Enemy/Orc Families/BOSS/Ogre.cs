using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : MonoBehaviour
{
    private bool isFacingRight = true;
    public int facingDirection = 1;
    public bool isGrounded;
    private Transform player;
    private Animator baton;
    private Animator anim;
    // public bool isJumping;
    // public bool isRunning;
    public bool isAttacking;
    public bool inRange;
    public bool isCharging;
    public bool fightMode;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 knockbackForce;

    [SerializeField] private Transform attackPos;
    [SerializeField] private Vector2 attackRangeSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform spinAttackPos;
    [SerializeField] private Vector2 spinAttackSize;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D RB;

    void Start()
    {
        baton = transform.Find("Baton").GetComponent<Animator>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        baton.SetBool("isSpin", isCharging);

        if(!isAttacking && !isCharging)
        {
            FlipTowardsPlayer();
        }

        if(isCharging)
        {
            SpinAttack();
        }

        CheckSurroundings();
    }


    public void Attack()
    {
        Collider2D player = Physics2D.OverlapBox(attackPos.position, attackRangeSize, 0, playerLayer);
        if(player != null)
        {
            player.GetComponent<PlayerMovement>().Knockback(knockbackForce); 
            player.GetComponent<Player>().TakeDamage(damage);    
        }
    }

    public void SpinAttack()
    {
        Collider2D player = Physics2D.OverlapBox(spinAttackPos.position, spinAttackSize, 0, playerLayer);
        if(player != null)
        {
            player.GetComponent<PlayerMovement>().Knockback(knockbackForce); 
            player.GetComponent<Player>().TakeDamage(damage);     
        }
    }


    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        inRange = Physics2D.OverlapBox(attackPos.position, attackRangeSize, 0, playerLayer);
    }

    public void AttackAnim()
    {
        baton.SetTrigger("Attack");
    }

    public void EnragedAttackAnim()
    {
        baton.SetTrigger("EnragedAttack");
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
        isFacingRight = !isFacingRight;
        knockbackForce.x *= -1;

        transform.Rotate(0,180,0);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.DrawWireCube(attackPos.position, attackRangeSize);
        Gizmos.DrawWireCube(spinAttackPos.position, spinAttackSize);
    }
}
