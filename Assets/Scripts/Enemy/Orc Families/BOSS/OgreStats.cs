using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreStats : MonoBehaviour
{
    [SerializeField] float health;
    private float currentHealth;


    [SerializeField] GameObject hurtParticle;
    // [SerializeField] GameObject dieParticle;

    private Animator anim;
    Vector2 startPosition;
    private Animator baton;
    Ogre ogre;
    int particleDirection;
    GameObject player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ogre = GetComponent<Ogre>();
        baton = transform.Find("Baton").GetComponent<Animator>();
        startPosition = transform.position;
        currentHealth = health;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(player.transform.position.x - transform.position.x < 0)
        {
            particleDirection = 1;
        } else
        {
            particleDirection = -1;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameObject particle = Instantiate(hurtParticle, transform.position, Quaternion.identity);
        var vel = particle.GetComponent<ParticleSystem>().velocityOverLifetime;
        vel.x = 10 * particleDirection;
        float half = health / 2;
        if(currentHealth <= half)
        {
            anim.SetBool("isEnraged", true);
        }

        if(currentHealth <= 0)
        {
            Die();
        }

        int rand = Random.Range(0,3);
        if(rand == 0)
        {
            FindObjectOfType<SoundManager>().Play("BodyStab1");
        } else if(rand == 1)
        {
            FindObjectOfType<SoundManager>().Play("BodyStab2");
        } else
        {
            FindObjectOfType<SoundManager>().Play("BodyStab3");
        }
    }

    void Die()
    {
        CinemachineShake[] cams =  FindObjectsOfType<CinemachineShake>();
        foreach (CinemachineShake cam in cams)
        {
            if(cam.enabled == true)
            {
                cam.ShakeCamera(3,1f);
            }
        }
        FindObjectOfType<SoundManager>().Play("BloodSplatter");
        FindObjectOfType<SoundManager>().Play("MonsterScream");
        FindObjectOfType<GameManager>().EndGame();
        // Destroy(gameObject);
    }

    public void Respawn()
    {
        ogre.fightMode = false;
        baton.SetBool("isSpin", false);
        anim.SetBool("isEnraged", false);
        anim.Play("OgreBoss-Idle");

        transform.position = startPosition;
        currentHealth = health;
    }
}
