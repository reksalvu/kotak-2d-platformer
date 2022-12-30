using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Overlays : MonoBehaviour
{
    TMP_Text score;
    TMP_Text life;
    int currentScore = 0;
    GameMaster gameMaster;


    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int knife;
    public int numOfKnife;

    public Image[] knives;
    public Sprite knifeSprite;

    void Start()
    {
        gameMaster = FindObjectOfType<GameMaster>();
        score = transform.Find("Coins Score").GetComponent<TMP_Text>();
        life = transform.Find("Lifes Count").GetComponent<TMP_Text>();
        // score = GetComponentInChildren<TMP_Text>();
        life.text = gameMaster.life.ToString();
        // score.text = currentScore.ToString();
    }

    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            } else
            {
                hearts[i].sprite = emptyHeart;
            }

            // if(i < numOfHearts)
            // {
            //     hearts[i].enabled = true;
            // } else
            // {
            //     hearts[i].enabled = false;
            // }
        }

        if(knife > numOfKnife)
        {
            knife = numOfKnife;
        }

        for(int i = 0; i < knives.Length; i++)
        {
            if(i < knife)
            {
                knives[i].enabled = true;
            } else
            {
                knives[i].enabled = false;
            }
        }
    }

    public void AddScore()
    {
        currentScore++;
        // score.text = currentScore.ToString();
    }

    public void DecreaseLife()
    {
        life.text = gameMaster.currentLife.ToString();
    }
}
