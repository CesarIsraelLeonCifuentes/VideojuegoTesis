using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class MenuCarrusel : MonoBehaviour
{
    //Textos de puntuación en la interfaz
    public Text txtPuntuacion1;
    public Text txtPuntuacion2;
    public Text txtPuntuacion3;
    public Text txtPuntuacion4;
    public Text txtPuntuacion5;
    public Text txtPuntuacion6;
    //Botones de la interfaz
    public Button btnCancion1;
    public Button btnCancion2;
    public Button btnCancion3;
    public Button btnCancion4;
    public Button btnCancion5;
    public Button btnCancion6;
    public Button btnVolverMenu;
    //gestor de la escena
    SceneLoadManager gestorEscena = new SceneLoadManager();
    //Instancia del socket para escuchar los gestos
    readSocket socket = new readSocket();
    //Cambio de sprite para efectos visuales de botones
    public Sprite pressedSprite;
    String gesto = "";
    int opcionActualF = 0;
    int opcionActualC = 0;
    //Instancio para obtener los eventos del sistema
    GameObject myEventSystem;
    //Gestión de audio
    public AudioSource m_MyAudioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioClip sound5;
    public AudioClip sound6;
    void Start()
    {
        m_MyAudioSource.volume = PlayerPrefs.GetFloat("volume");
        myEventSystem = GameObject.Find("EventSystem");
        btnCancion1.Select();
        btnCancion1.onClick.AddListener(IrCancion1);
        btnCancion2.onClick.AddListener(IrCancion2);
        btnCancion3.onClick.AddListener(IrCancion3);
        btnCancion4.onClick.AddListener(IrCancion4);
        btnCancion5.onClick.AddListener(IrCancion5);
        btnCancion6.onClick.AddListener(IrCancion6);
        btnVolverMenu.onClick.AddListener(VolverMenu);
        //Inicializa la primera canción
        m_MyAudioSource.clip = sound1;
        m_MyAudioSource.Play();
        //Puntuaciones
        txtPuntuacion1.text = "Mejor puntuación: "+PlayerPrefs.GetInt("puntuacionCancion1").ToString();
        txtPuntuacion2.text = "Mejor puntuación: "+PlayerPrefs.GetInt("puntuacionCancion2").ToString();
        txtPuntuacion3.text = "Mejor puntuación: "+PlayerPrefs.GetInt("puntuacionCancion3").ToString();
        txtPuntuacion4.text = "Mejor puntuación: "+PlayerPrefs.GetInt("puntuacionCancion4").ToString();
        txtPuntuacion5.text = "Mejor puntuación: "+PlayerPrefs.GetInt("puntuacionCancion5").ToString();
        txtPuntuacion6.text = "Mejor puntuación: "+PlayerPrefs.GetInt("puntuacionCancion6").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        try{
            gesto = socket.Update();
            if(gesto == "WaveIn" && opcionActualC >0){
                opcionActualC -= 1; 
                ActualizarOpcion();
            }else if (gesto == "WaveOut" && opcionActualC <2){
                opcionActualC += 1; 
                ActualizarOpcion();
            }else if (gesto == "Up" && opcionActualF >0){
                opcionActualF -= 1; 
                ActualizarOpcion();
            }else if (gesto == "Down" && opcionActualF <1){
                opcionActualF += 1; 
                ActualizarOpcion();
            }else if (gesto == "Open" || gesto == "Forward"){
                RealizarOpcion();
            }
            else if (gesto == "Pinch"){
                VolverMenu();
            }
        }catch (Exception ex)
        {
            socket.Start();
        }
    }

    void ActualizarOpcion(){
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        switch(opcionActualC)
        {
            case 0:
                switch(opcionActualF){
                    case 0:
                        btnCancion1.Select();
                        m_MyAudioSource.clip = sound1;    
                        break;
                    case 1:
                        btnCancion4.Select();
                        m_MyAudioSource.clip = sound4;    
                        break;
                    default:
                        btnCancion1.Select();
                        m_MyAudioSource.clip = sound1;    
                        break;
                }   
                break;
            case 1: 
                switch(opcionActualF){
                    case 0:
                        btnCancion2.Select();
                        m_MyAudioSource.clip = sound2;    
                        break;
                    case 1:
                        btnCancion5.Select();
                        m_MyAudioSource.clip = sound5;    
                        break;
                    default:
                        btnCancion1.Select();
                        m_MyAudioSource.clip = sound2;    
                        break;
                } 
                break;
            case 2: 
                switch(opcionActualF){
                    case 0:
                        btnCancion3.Select();
                        m_MyAudioSource.clip = sound3;    
                        break;
                    case 1:
                        btnCancion6.Select();
                        m_MyAudioSource.clip = sound6;    
                        break;
                    default:
                        btnCancion1.Select();
                        m_MyAudioSource.clip = sound3;    
                        break;
                } 
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
        PlayerPrefs.SetFloat("tiempoValidez", 8.00f);
        gestorEscena.LoadNextScene(2); 
    }
    void IrCancion3(){
        PlayerPrefs.SetInt("cancionBaile", 3);
        PlayerPrefs.SetFloat("tiempoValidez", 6.00f);
        gestorEscena.LoadNextScene(2); 
    }
    void IrCancion4(){
        PlayerPrefs.SetInt("cancionBaile", 4);
        PlayerPrefs.SetFloat("tiempoValidez", 6.00f);
        gestorEscena.LoadNextScene(2); 
    }
    void IrCancion5(){
        PlayerPrefs.SetInt("cancionBaile", 5);
        PlayerPrefs.SetFloat("tiempoValidez", 5.00f);
        gestorEscena.LoadNextScene(2); 
    }
    void IrCancion6(){
        PlayerPrefs.SetInt("cancionBaile", 6);
        PlayerPrefs.SetFloat("tiempoValidez", 5.00f);
        gestorEscena.LoadNextScene(2); 
    }

    void VolverMenu(){
        gestorEscena.LoadNextScene(0);
    }

    void RealizarOpcion(){
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        switch(opcionActualC)
        {
            case 0:
                switch(opcionActualF){
                    case 0:
                        btnCancion1.image.sprite = pressedSprite;
                        btnCancion1.onClick.Invoke();    
                        break;
                    case 1:
                        btnCancion4.image.sprite = pressedSprite;
                        btnCancion4.onClick.Invoke();  
                        break;
                    default:
                        btnCancion1.image.sprite = pressedSprite;
                        btnCancion1.onClick.Invoke();    
                        break;
                }   
                break;
            case 1: 
                switch(opcionActualF){
                    case 0:
                        btnCancion2.image.sprite = pressedSprite;
                        btnCancion2.onClick.Invoke();    
                        break;
                    case 1:
                        btnCancion5.image.sprite = pressedSprite;
                        btnCancion5.onClick.Invoke();    
                        break;
                    default:
                        btnCancion2.image.sprite = pressedSprite;
                        btnCancion2.onClick.Invoke();   
                        break;
                } 
                break;
            case 2: 
                switch(opcionActualF){
                    case 0:
                        btnCancion3.image.sprite = pressedSprite;
                        btnCancion3.onClick.Invoke();    
                        break;
                    case 1:
                        btnCancion6.image.sprite = pressedSprite;
                        btnCancion6.onClick.Invoke();    
                        break;
                    default:
                        btnCancion3.image.sprite = pressedSprite;
                        btnCancion3.onClick.Invoke();    
                        break;
                } 
                break;
            default:
                btnCancion1.image.sprite = pressedSprite;
                btnCancion1.onClick.Invoke(); 
                break;
        }
    }
}
