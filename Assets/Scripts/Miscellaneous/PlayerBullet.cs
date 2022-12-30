using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] GameObject knifeParticle;
    public float direction;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Poo poo = other.gameObject.GetComponent<Poo>();
            if(poo != null)
            {
                poo.Die();
            }
        
            GoblinStats goblin = other.gameObject.GetComponent<GoblinStats>();
            if(goblin != null)
            {
                goblin.TakeDamage(damage);
            }

            OrcWarriorStats orcWarrior = other.gameObject.GetComponent<OrcWarriorStats>();
            if(orcWarrior != null)
            {
                orcWarrior.TakeDamage(damage);
            }

            OrcShamanStats orcShaman = other.gameObject.GetComponent<OrcShamanStats>();
            if(orcShaman != null)
            {
                orcShaman.TakeDamage(damage);
            }

            MaskedOrcStats maskedOrc = other.gameObject.GetComponent<MaskedOrcStats>();
            if(maskedOrc != null)
            {
                maskedOrc.TakeDamage(damage);
            }

            OgreStats ogre = other.gameObject.GetComponent<OgreStats>();
            if(ogre != null)
            {
                ogre.TakeDamage(damage);
            }
            
            Destroy(gameObject);
        }

        
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Spike")
        {
            GameObject particle = Instantiate(knifeParticle, transform.position, Quaternion.identity);
            var vel = particle.GetComponent<ParticleSystem>().velocityOverLifetime;
            vel.x = 5 * direction;
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}
