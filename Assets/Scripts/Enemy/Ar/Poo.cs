using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poo : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private bool isTouchingWall;
    private bool isGrounded;
    private int facingDirection;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private GameObject deadParticle1;
    [SerializeField] private GameObject deadParticle2;

    private Rigidbody2D RB;

    Vector2 startPosition;


    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("Ground Check");
        wallCheck = transform.Find("Wall Check");
        facingDirection = -1;
        startPosition = transform.position;
    }


    private void FixedUpdate()
    {
        CheckCollision();

        RB.velocity = new Vector2(speed * facingDirection, RB.velocity.y);
    } 

    public void Respawn()
    {
        transform.position = startPosition;
    }

    public void Die()
    {
        FindObjectOfType<SoundManager>().Play("BloodSplatter");
        GameObject particle1 = Instantiate(deadParticle1, transform.position, Quaternion.identity);
        particle1.transform.Rotate(-90,0,0);
        GameObject particle2 = Instantiate(deadParticle2, transform.position, Quaternion.identity);
        particle2.transform.Rotate(-90,0,0);
        CinemachineShake[] cams =  FindObjectsOfType<CinemachineShake>();
        foreach (CinemachineShake cam in cams)
        {
            if(cam.enabled == true)
            {
                cam.ShakeCamera(3,.2f);
            }
        }
        gameObject.SetActive(false);
    }

    private void CheckCollision()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, wallLayer);

        if(isTouchingWall || !isGrounded)
        {
            facingDirection *= -1;
            transform.Rotate(0f,180.0f,0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + -wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
