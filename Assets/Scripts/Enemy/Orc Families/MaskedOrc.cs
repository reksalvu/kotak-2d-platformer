using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedOrc : MonoBehaviour
{
    private Transform shootPoint;
    private Transform gunHolder;
    private Transform player;
    private Transform eye;
    [SerializeField] private GameObject arrow;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float shootTime;
    private float shootTimeCounter;
    private Vector2 direction;


    private bool isFacingRight = true;
    private bool canSeePlayer;
    private int facingDirection = 1;


    Vector2 startPosition;


    MaskedOrcStats orc;

    private void Start()
    {
        orc = GetComponent<MaskedOrcStats>();
        shootPoint = transform.Find("Gun Holder").transform.Find("Shoot Point");
        gunHolder = transform.Find("Gun Holder");
        eye = transform.Find("Eye");
        player = FindObjectOfType<Player>().transform;
        startPosition = transform.position;
    }


    private void Update()
    {
        shootTimeCounter -= Time.deltaTime;

        direction = player.position - gunHolder.position;

        FlipTowardsPlayer();
        CheckSurroundings();
        
        if(canSeePlayer)
        {
            FacePlayer();
            if(shootTimeCounter <= 0)
            {
                Shoot();
                shootTimeCounter = shootTime;
            }
        }
    }

    void CheckSurroundings()
    {
        RaycastHit2D hit = Physics2D.Raycast(eye.position, direction);

        if(hit.collider.tag == "Player")
        {
            canSeePlayer = true;
        } else 
        {
            canSeePlayer = false;
        }
    }

    void Shoot()
    {
        int rand = Random.Range(0,3);
        if(rand == 0)
        {
            FindObjectOfType<SoundManager>().Play("WhooshAir1");
        } else if(rand == 1)
        {
            FindObjectOfType<SoundManager>().Play("WhooshAir2");
        } else
        {
            FindObjectOfType<SoundManager>().Play("WhooshAir3");
        }
        GameObject arrowIns = Instantiate(arrow, shootPoint.position, shootPoint.rotation);
        arrowIns.GetComponent<Rigidbody2D>().AddForce(arrowIns.transform.right * arrowSpeed, ForceMode2D.Impulse);
        Destroy(arrowIns,3f);
    }

    void FacePlayer()
    {
        gunHolder.transform.right = direction;
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

        transform.Rotate(0,180,0);
    }

    public void Respawn()
    {
        transform.position = startPosition;
        orc.OnEnable();
    }
}
