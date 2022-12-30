using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(1);
            float playerPos = other.transform.position.x - transform.position.x;
            if(playerPos < 0)
            {
                other.gameObject.GetComponent<PlayerMovement>().Knockback(new Vector2(-20,-10));
            } else if(playerPos > 0)
            {
                other.gameObject.GetComponent<PlayerMovement>().Knockback(new Vector2(20,10));
            }
        }
    }
}
