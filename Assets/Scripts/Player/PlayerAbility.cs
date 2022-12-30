using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    private GameObject currentTeleporter;
    private float readyForNextShot;
    [SerializeField] private int maxKnives;
    private int knivesLeft;
    [SerializeField] private float rechargeTime;
    private float rechargeTimeCounter;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;
    private Animator anim;
    private Overlays overlays;

    void Start()
    {
        overlays = FindObjectOfType<Overlays>();
        anim = GetComponent<Animator>();
        knivesLeft = maxKnives;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            }
        } 

       if(Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0))
        {
            if(Time.time > readyForNextShot && knivesLeft > 0)
            {
                readyForNextShot = Time.time + 1/fireRate;
                knivesLeft--;
                Shoot();
            }
        }

        if(knivesLeft < 5)
        {
            rechargeTimeCounter -= Time.deltaTime;
            if(rechargeTimeCounter <= 0)
            {
                knivesLeft++;
                rechargeTimeCounter = rechargeTime;
            }
        } else
        {
            rechargeTimeCounter = rechargeTime;
        }

        overlays.knife = knivesLeft;

    }


    void Shoot()
    {
        anim.SetTrigger("Hit");
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
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody2D bulletRB = BulletIns.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        if(bulletRB.velocity.x > 0)
        {
            BulletIns.GetComponent<PlayerBullet>().direction = -1;
        } else
        {
            BulletIns.GetComponent<PlayerBullet>().direction = 1;
        }
        BulletIns.transform.Rotate(0,0,-90);
        Destroy(BulletIns,3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Teleporter"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
