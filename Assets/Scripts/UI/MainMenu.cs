using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        FindObjectOfType<SoundManager>().Play("ClickPen");
        SceneManager.LoadScene(1);
    }

    public void ClickSound()
    {
        FindObjectOfType<SoundManager>().Play("ClickPen");
    }

    public void Quit()
    {   
        FindObjectOfType<SoundManager>().Play("ClickPen");
        Application.Quit();
    }
}
