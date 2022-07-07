using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class MenuCarrusel : MonoBehaviour
{
    //botones
    public Button btnCancion1;
    public Button btnCancion2;
    public Button btnCancion3;
    public Button btnVolverMenu;
    //gestor de la escena
    SceneLoadManager gestorEscena = new SceneLoadManager();
    //Instancia del socket para escuchar los gestos
    readSocket socket = new readSocket();
    //Cambio de sprite para efectos visuales de botones
    public Sprite pressedSprite;
    String gesto = "";
    int opcionActual = 0;
    //Instancio para obtener los eventos del sistema
    GameObject myEventSystem;
    //Gestión de audio
    public AudioSource m_MyAudioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    void Start()
    {
        m_MyAudioSource.volume = PlayerPrefs.GetFloat("volume");
        myEventSystem = GameObject.Find("EventSystem");
        socket.Start();
        btnCancion1.Select();
        btnCancion1.onClick.AddListener(IrCancion1);
        btnCancion2.onClick.AddListener(IrCancion2);
        btnCancion3.onClick.AddListener(IrCancion3);
        btnVolverMenu.onClick.AddListener(VolverMenu);
        //Inicializa la primera canción
        m_MyAudioSource.clip = sound1;
        m_MyAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        gesto = socket.Update();
        if(gesto == "WaveIn" && opcionActual >0){
            opcionActual -= 1; 
            ActualizarOpcion();
        }else if (gesto == "WaveOut" && opcionActual <2){
            opcionActual += 1; 
            ActualizarOpcion();
        }else if (gesto == "Open"){
            RealizarOpcion();
        }
        else if (gesto == "Pinch"){
            opcionActual = 3; 
            RealizarOpcion();
        }
    }

    void ActualizarOpcion(){
        switch(opcionActual)
        {
            case 0: 
                btnCancion1.Select();
                m_MyAudioSource.clip = sound1;    
                break;
            case 1: 
                btnCancion2.Select();
                m_MyAudioSource.clip = sound2;
                break;
            case 2: 
                btnCancion3.Select();
                m_MyAudioSource.clip = sound3;
                break;
            default:
                btnCancion1.Select();
                break;
        }
        m_MyAudioSource.Play();
    }
    void IrCancion1(){
        PlayerPrefs.SetInt("cancionBaile", 1);
        PlayerPrefs.SetFloat("tiempoValidez", 8.00f);
        gestorEscena.LoadNextScene(2);
    }
    void IrCancion2(){
        PlayerPrefs.SetInt("cancionBaile", 2);
        PlayerPrefs.SetFloat("tiempoValidez", 6.00f);
        gestorEscena.LoadNextScene(3);
    }
    void IrCancion3(){
        PlayerPrefs.SetInt("cancionBaile", 3);
        PlayerPrefs.SetFloat("tiempoValidez", 5.00f);
        gestorEscena.LoadNextScene(4);
    }
    void VolverMenu(){
        gestorEscena.LoadNextScene(0);
    }

    void RealizarOpcion(){
        switch(opcionActual)
        {
            case 0: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnCancion1.image.sprite = pressedSprite;
                btnCancion1.onClick.Invoke();
                break;
            case 1: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnCancion2.image.sprite = pressedSprite;
                btnCancion2.onClick.Invoke();
                break;
            case 2: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnCancion3.image.sprite = pressedSprite;
                btnCancion3.onClick.Invoke();
                break;
            case 3: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnVolverMenu.image.sprite = pressedSprite;
                btnVolverMenu.onClick.Invoke();
                break;
            default:
                btnCancion1.Select();
                break;
        }
    }
}
