using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager instance;

    public float startMusicVolume;
    public float startSfxVolume;


    // private float pauseVolume = 0.050f;

    private AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if(s.name == "BackMusic")
            {
                PlayerPrefs.SetFloat("MusicVolume", s.source.volume);
                startMusicVolume = s.source.volume;
            } else if(s.name == "Jump")
            {
                PlayerPrefs.SetFloat("SfxVolume", s.source.volume);
                startSfxVolume = s.source.volume;
            }
        }

        Play("BackMusic");
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        if(PauseMenu.gameIsPaused)
        {
            audioSource.Pause();
        } else 
        {
            audioSource.UnPause();
        }


    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }

        s.source.Play();
    }    

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }

        s.source.Stop();
    }    



    public void SetMusicVolume(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "BackMusic");
        s.source.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            if(s.name == "Jump" || s.name == "Dead" || s.name == "Checkpoint" || s.name == "PooDead" || s.name == "Finish" || s.name == "Click" || s.name == "Coin" || s.name == "WhooshAir1" || s.name == "WhooshAir2" || s.name == "WhooshAir3" || s.name == "BodyStab1" || s.name == "BodyStab2" || s.name == "BodyStab3" || s.name == "BodySplatter" || s.name == "WeaponSwing1" || s.name == "WeaponSwing2" || s.name == "WeaponSwing3" || s.name == "MonsterScream" || s.name == "MonsterGrowl1" || s.name == "MonsterGrowl2" || s.name == "Gulp" )
            {
                s.source.volume = volume;
                PlayerPrefs.SetFloat("SfxVolume", volume);
            }
        }
    }
}
