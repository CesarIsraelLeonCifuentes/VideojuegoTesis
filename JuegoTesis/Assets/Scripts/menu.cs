using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class menu : MonoBehaviour
{   public AudioSource AudioSource;
    //botones
    public Button btnJugar;
    public Button btnVolumen;
    public Button btnPuntuaciones;
    public Button btnSalir;
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
    //Volumen
    float musicVolumen = 0.1f;
    void Start()
    {
        //Configuración del volumen
        musicVolumen = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = musicVolumen;
        myEventSystem = GameObject.Find("EventSystem");
        socket.Start();
        btnJugar.Select();
        btnJugar.onClick.AddListener(IrCarruselCanciones);
        btnSalir.onClick.AddListener(SalirAplicacion);
        btnVolumen.onClick.AddListener(IrConfiguracionVolumen);
    }

    // Update is called once per frame
    void Update()
    {
        gesto = socket.Update();
        if(gesto == "Up" && opcionActual >0){
            opcionActual -= 1; 
            ActualizarOpcion();
        }else if (gesto == "Down" && opcionActual <3){
            opcionActual += 1; 
            ActualizarOpcion();
        }else if (gesto == "Open"){
            RealizarOpcion();
        }
    }

    void IrCarruselCanciones(){

        gestorEscena.LoadNextScene(1);
    }

    void SalirAplicacion(){
        Application.Quit();
    }
    void IrConfiguracionVolumen(){
        gestorEscena.LoadNextScene(5);
    }

    void ActualizarOpcion(){
        switch(opcionActual)
        {
            case 0: 
                btnJugar.Select();
                break;
            case 1: 
                btnVolumen.Select();
                break;
            case 2: 
                btnPuntuaciones.Select();
                break;
            case 3: 
                btnSalir.Select();
                break;
            default:
                btnJugar.Select();
                break;
        }
    }

    void RealizarOpcion(){
        switch(opcionActual)
        {
            case 0: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnJugar.image.sprite = pressedSprite;
                btnJugar.onClick.Invoke();

                break;
            case 1: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnVolumen.image.sprite = pressedSprite;
                btnVolumen.onClick.Invoke();
                break;
            case 2: 

                break;
            case 3: 
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                btnSalir.image.sprite = pressedSprite;
                SalirAplicacion();
                break;
            default:
                btnJugar.Select();
                break;
        }
    }

}
