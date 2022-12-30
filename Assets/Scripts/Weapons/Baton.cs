using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baton : MonoBehaviour
{
    Ogre ogre;

    void Start()
    {
        ogre = FindObjectOfType<Ogre>();
    }

    void Attack()
    {
        ogre.Attack();
    }

    void Sound()
    {
        int rand = Random.Range(0,3);
        if(rand == 0)
        {
            FindObjectOfType<SoundManager>().Play("WeaponSwing1");
        } else if(rand == 1)
        {
            FindObjectOfType<SoundManager>().Play("WeaponSwing2");
        } else
        {
            FindObjectOfType<SoundManager>().Play("WeaponSwing3");
        }
    }
}
