using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
public class menuRespuestaPuntuacion : MonoBehaviour
{
    //botones
    public Button btnVolver;
    //textos
    public Text txtPuntuacion;
    public Text txtMensaje;
    //Origen del audio
    public AudioSource audioSource;
    //gestor de la escena
    SceneLoadManager gestorEscena = new SceneLoadManager();
    //Gestos
    String gesto;
    //Instancia del socket para escuchar los gestos
    readSocket socket = new readSocket();
    // Start is called before the first frame update
    void Start()
    {
        txtPuntuacion.text = (PlayerPrefs.GetInt("puntuacionObtenida")).ToString();
        txtMensaje.text = PlayerPrefs.GetString("mensajeCancion");
        btnVolver.onClick.AddListener(VolverCarruselCanciones);
    }

    // Update is called once per frame
    void Update()
    {
        try{
            gesto = socket.Update();
            if(!audioSource.isPlaying || gesto == "Pinch"){
                VolverCarruselCanciones();
            }
        }catch (Exception ex)
        {
            socket.Start();
        }
        
    }
    void VolverCarruselCanciones(){
        gestorEscena.LoadNextScene(1);
    }
}