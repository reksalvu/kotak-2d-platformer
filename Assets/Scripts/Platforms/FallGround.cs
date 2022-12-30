using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGround : MonoBehaviour
{

    public float fallDelay;

    Rigidbody2D RB;

    Vector2 startPosition;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if(transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        RB.bodyType = RigidbodyType2D.Static;
        transform.position = startPosition;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall()); 
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        RB.bodyType = RigidbodyType2D.Dynamic;
        RB.gravityScale = 5;
    }
}
