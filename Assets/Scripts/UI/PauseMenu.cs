using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool gameIsPaused = false;

    private Transform pauseMenu; 

    void Start()
    {
        pauseMenu = transform.Find("Pause Menu");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Menu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
