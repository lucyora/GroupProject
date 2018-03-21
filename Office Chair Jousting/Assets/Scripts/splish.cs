using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class splish : MonoBehaviour
{
    public AudioClip sploosh;   

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = sploosh;
    }

    void OnCollisionEnter()  //Plays Sound Whenever collision detected
    {
        GetComponent<AudioSource>().Play();
    }
}
