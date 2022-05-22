using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class soundManager : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    public AudioClip sound1;
    
    public AudioClip sound2;
    
    public AudioClip sound3;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up"))
        {
            m_MyAudioSource.clip = sound1;
            m_MyAudioSource.Play();
        }
        if(Input.GetKey("down"))
        {
            m_MyAudioSource.clip = sound2;
            m_MyAudioSource.Play();
        }
        if(Input.GetKey("left"))
        {
            m_MyAudioSource.clip = sound3;
            m_MyAudioSource.Play();
        }
    }
}
