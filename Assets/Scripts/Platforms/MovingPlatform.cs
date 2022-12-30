using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Rigidbody2D RB;

    public float speed;
    public int startPoint;
    public Transform[] points;

    int i;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        transform.position = points[startPoint].position;    
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
