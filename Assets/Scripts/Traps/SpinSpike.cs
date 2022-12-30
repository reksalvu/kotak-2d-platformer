using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpike : MonoBehaviour
{
    [SerializeField] private float spinSpeed;

    public float speed;
    public int startPoint;
    public Transform[] points;

    private Rigidbody2D RB;

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
        transform.Rotate(0,0,spinSpeed);
    }
}
