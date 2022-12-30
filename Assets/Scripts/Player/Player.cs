using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 5;
    private int currentHealth;
    public bool isInvulnerable;

    private GameManager GM;
    private Animator anim;
    private PlayerMovement PM;
    [SerializeField] private GameObject hurtParticle;

    [Header("TimeControllerSettings")]
    public float TimeScale;

    private float StartTimeScale;
    private float StartFixedDeltaTime;
    private Overlays overlays;

    public bool isFightingBoss;



    private void Start()
    {
        overlays = FindObjectOfType<Overlays>();
        anim = GetComponent<Animator>();
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        PM = FindObjectOfType<PlayerMovement>();
        currentHealth = health;
        StartTimeScale = Time.timeScale;
        StartFixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if(transform.position.y < -30)
        {
            Die();
        }
        anim.SetBool("isInvulnerable", isInvulnerable);
        overlays.health = currentHealth;
    }


    public void TakeDamage(int damage)
    {
        if(isInvulnerable)
        {
            return;
        }
        anim.SetTrigger("Hit");
        currentHealth -= damage;

        Instantiate(hurtParticle, transform.position, Quaternion.identity);
        // StartCoroutine(Slow(1));
        StartCoroutine(Invulnerable(3));
        if(currentHealth <= 0)
        {   
            if(isFightingBoss)
            {
                FindObjectOfType<SoundManager>().Stop("BattleTheme");
                FindObjectOfType<SoundManager>().Play("BackMusic");
                isFightingBoss = false;
            }
            Die();
        }

        CinemachineShake[] cams =  FindObjectsOfType<CinemachineShake>();
        foreach (CinemachineShake cam in cams)
        {
            if(cam.enabled == true)
            {
                cam.ShakeCamera(3,.2f);
            }
        }   
    }

    // public void Knockback(Vector2 knockbackForce)
    // {
    //     PM.Knockback(knockbackForce);
    // }

    private IEnumerator Slow(float time)
    {
        StartSlowMotion();
        yield return new WaitForSeconds(time);
        StopSlowMotion();
        StartCoroutine(Invulnerable(3));
    }

    private IEnumerator Invulnerable(float time)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(time);
        isInvulnerable = false;
    }

    private void Die()
    {
        currentHealth = health;
        overlays.health = overlays.numOfHearts;
        anim.Play("Player-Idle");
        isInvulnerable = false;
        CinemachineShake[] cams =  FindObjectsOfType<CinemachineShake>();
        foreach (CinemachineShake cam in cams)
        {
            if(cam.enabled == true)
            {
                cam.ShakeCamera(3,1f);
            }
        } 
        GM.PlayerDie();
        FindObjectOfType<SoundManager>().Play("BloodSplatter");
        gameObject.SetActive(false);
    }


    private void StartSlowMotion()
    {
        Time.timeScale = TimeScale;
        Time.fixedDeltaTime = StartFixedDeltaTime * TimeScale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = StartTimeScale;
        Time.fixedDeltaTime = StartFixedDeltaTime;
    }

    public void DrinkPotion()
    {
        currentHealth = health;
    }
}
