using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcShamanStats : MonoBehaviour
{
    [SerializeField] float health;
    private float currentHealth;


    [SerializeField] GameObject hurtParticle;
    [SerializeField] GameObject deadParticle1;
    [SerializeField] GameObject deadParticle2;

    private int particleDirection;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = health;
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
        if(currentHealth <= 0)
        {
            Die();
        }
        GameObject particle = Instantiate(hurtParticle, transform.position, Quaternion.identity);
        var vel = particle.GetComponent<ParticleSystem>().velocityOverLifetime;
        vel.x = 10 * particleDirection;
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
                cam.ShakeCamera(3,.5f);
            }
        }
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        currentHealth = health;
    }
}