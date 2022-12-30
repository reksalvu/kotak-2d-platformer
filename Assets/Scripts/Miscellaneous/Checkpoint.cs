using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager GM;
    private SpriteRenderer SR;

    private bool isChecked;


    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        SR = GetComponent<SpriteRenderer>();
        isChecked = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(!isChecked)
            {
                isChecked = true;
                SR.color = new Color(255,255,0,255);
                GM.lastCheckPointPos = transform.position;
                FindObjectOfType<SoundManager>().Play("Checkpoint");
            }
        }
    }
}
