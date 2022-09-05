using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
public class instrucciones1 : MonoBehaviour
{
    public AudioSource AudioSource;
    //botones
    public Button continuar;
    SceneLoadManager gestorEscena = new SceneLoadManager();
    //Gestos
    String gesto;
    //Instancia del socket para escuchar los gestos
    readSocket socket = new readSocket();
    // Start is called before the first frame update
      //Volumen
    float musicVolumen = 0.1f;
    void Start()
    {
        //Configuración del volumen
        //Se obtiene el volumen definido en anteriores sesiones
        musicVolumen = PlayerPrefs.GetFloat("volume");
        //Se configura el sonido en el juego
        AudioSource.volume = musicVolumen;
       continuar.onClick.AddListener(continuarInstruccion);
    }

    // Update is called once per frame
    void Update()
    {
        try{
            gesto = socket.Update();
            if(gesto == "Pinch"){
                continuarInstruccion();
            }
        }catch (Exception ex)
        {
            socket.Start();
        }
        
    }
    void continuarInstruccion(){
        gestorEscena.LoadNextScene(6);
    }
}
