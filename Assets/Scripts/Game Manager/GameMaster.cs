using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public int life;
    public int currentLife;
    bool gameOver;

    Overlays overlays;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        overlays = FindObjectOfType<Overlays>();
        currentLife = life;
    }

    public bool Dead()
    {
        gameOver = false;
        currentLife--;
        overlays.DecreaseLife();
        if(currentLife <= 0)
        {
            gameOver = true;
        }
        return gameOver;
    }
}
