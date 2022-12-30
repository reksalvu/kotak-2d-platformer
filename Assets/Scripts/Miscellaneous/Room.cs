using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    private CinemachineShake cinemachineShake;


    void Start()
    {
        cinemachineShake = GetComponentInChildren<CinemachineShake>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            virtualCam.enabled = true;
            cinemachineShake.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            virtualCam.enabled = false;
            cinemachineShake.enabled = false;
        }
    }
}
