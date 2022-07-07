using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class soundManager : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    public AudioClip sound1;
    
    public AudioClip sound2;
    
    public AudioClip sound3;

    void Start()
    {
        //Fetch the AudioSource from the GameObject

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up"))
        {
            m_MyAudioSource.clip = sound1;
            m_MyAudioSource.Play();
            //nombreCancion.text = "Canción 1";
        }else if(Input.GetKey("down"))
        {
            m_MyAudioSource.clip = sound2;
            m_MyAudioSource.Play();
            //nombreCancion.text = "Canción 2";
        }else if(Input.GetKey("left"))
        {
            m_MyAudioSource.clip = sound3;
            m_MyAudioSource.Play();
            //nombreCancion.text = "Canción 3";
        }else if(Input.GetKey("right")){
             m_MyAudioSource.Stop();
        }
    }

}
