using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    private Rigidbody2D RB;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);

            float playerPos = other.transform.position.x - transform.position.x;
            if(playerPos < 0)
            {
                other.gameObject.GetComponent<PlayerMovement>().Knockback(new Vector2(-20,0));
            } else if(playerPos > 0)
            {
                other.gameObject.GetComponent<PlayerMovement>().Knockback(new Vector2(20,0));
            }

            Destroy(gameObject);
        }
    }
}
