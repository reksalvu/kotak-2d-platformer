using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public float amplitude = 0.2f; 
    public float frequency = 0.5f;
    
    
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    private void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency)* amplitude;
        transform.position = tempPos;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Overlays").GetComponent<Overlays>().AddScore();
            FindObjectOfType<SoundManager>().Play("Coin");
            Destroy(gameObject);
        }
    }
}
