using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarrior : MonoBehaviour
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



    [Header("ATTACK")]
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 knockbackForce;
    [SerializeField] private float startTimeBtwAttack;
    private float timeBtwAttack;
    private Animator axe;


    [Header("VISION")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] Vector2 attackDistance;
    [SerializeField] LayerMask playerLayer;
    private Transform player;
    private bool canSeePlayer;
    private bool inAttackDistance;

    private Rigidbody2D RB;
    private Animator anim;

    Vector2 startPosition;
    float startSpeed;

    OrcWarriorStats orc;


    void Start()
    {
        orc = GetComponent<OrcWarriorStats>();
        RB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        axe = transform.Find("Weapon").GetComponent<Animator>();
        startPosition = transform.position;
        startSpeed = moveSpeed;
    }


    void Update()
    {
        timeBtwAttack -= Time.deltaTime;

        CheckSurroundings();
        AnimationController();
    }

    void FixedUpdate()
    {
        if(!canSeePlayer || !inAttackDistance)
        {
            Patrolling();
            timeBtwAttack = startTimeBtwAttack;
        }  
        // if(canSeePlayer && !inAttackDistance)
        // {
        //     FlipTowardsPlayer();
        //     RB.velocity = new Vector2(moveSpeed * facingDirection, RB.velocity.y);
        // } 

        if(inAttackDistance && timeBtwAttack <= 0)
        {
            Attack();
            timeBtwAttack = startTimeBtwAttack;    
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

    void Attack()
    {
        int rand = Random.Range(0,3);
        if(rand == 0)
        {
            FindObjectOfType<SoundManager>().Play("WeaponSwing1");
        } else if(rand == 1)
        {
            FindObjectOfType<SoundManager>().Play("WeaponSwing2");
        } else
        {
            FindObjectOfType<SoundManager>().Play("WeaponSwing3");
        }
        axe.SetTrigger("Attack");
        Collider2D player = Physics2D.OverlapCircle(attackPos.position, attackRange, playerLayer);
        if(player != null)
        {
            player.GetComponent<PlayerMovement>().Knockback(knockbackForce); 
            player.GetComponent<Player>().TakeDamage(damage);       
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
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);
        inAttackDistance = Physics2D.OverlapBox(transform.position, attackDistance, 0, playerLayer);
    }

    void AnimationController()
    {
        if(!inAttackDistance)
        {
            anim.SetBool("isWalking", true);
        } else if(inAttackDistance)
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void IncreaseOrcSpeed(float amountSpeed)
    {
        FlipTowardsPlayer();
        moveSpeed += amountSpeed;
    }

    public void Respawn()
    {
        transform.position = startPosition;
        moveSpeed = startSpeed;
        orc.OnEnable();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, attackDistance);
    }

    void OnEnable()
    {
        Invoke("Reset",0.5f);
    }

    void Reset()
    {
        moveSpeed = startSpeed;
    }

}
