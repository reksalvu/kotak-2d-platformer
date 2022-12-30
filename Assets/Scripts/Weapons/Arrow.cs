using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground" || other.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);

            float playerPos = other.transform.position.x - transform.position.x;
            if(playerPos < 0)
            {
                other.GetComponent<PlayerMovement>().Knockback(new Vector2(-20,0));
            } else if(playerPos > 0)
            {
                other.GetComponent<PlayerMovement>().Knockback(new Vector2(20,0));
            }

            Destroy(gameObject);
        }
    }
}
