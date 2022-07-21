using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using UnityEngine.UI;
using System.Text;
using Random=UnityEngine.Random;
public class controladorBaile : MonoBehaviour
{
    //RawImage
    public RawImage rawImage;
    //Video
    public VideoPlayer videoPlayer;
    public VideoClip imgLeft;
    public VideoClip imgRight;
    public VideoClip imgUp;
    public VideoClip imgDown;
    public VideoClip imgOpen;
    public VideoClip imgPinch;
    public VideoClip imgWaveIn;
    public VideoClip imgWaveOut;
    public VideoClip imgForward;
    public VideoClip imgBackward;

    //Colores de texto
    Color colorRojo = Color.red;
    Color colorVerde = Color.green;
    //Canciones
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioClip sound5;
    public AudioClip sound6;
    //Interfaz
    public Text txtPuntuacion;
    public Text nombreGesto;
    public Text nombreCancion;
    public Text txtPausa;
    public Text txtMensaje;
    // Use this for initialization
    int puntuacionValor;
    String gesto;
    int estadoActual = 0;
    //Animator
    private Animator animator;
    //Instancia del socket para escuchar los gestos
    readSocket socket = new readSocket();
    //Origen del audio
    public AudioSource audioSource;
    //gestor de la escena
    SceneLoadManager gestorEscena = new SceneLoadManager();
    int numeroCancion;
    //Json de gestos y sus tiempos
    public TextAsset gestosJSON;
    //Clase de gesto
    [System.Serializable]
    public class Gesto{
        public string nombre;
        public float tiempo;
    }
    [System.Serializable]
    public class ListaGestos{
        public Gesto[] gesto;
    }
    public ListaGestos listaGestos = new ListaGestos(); 
    //tiempo del siguiente gesto
    float tiempoSiguienteGesto = 10.00f;
    float tiempoValidez;
    //Gesto a realizar
    String gestoARealizar;
    int idGestoAnterior;
    int idGesto;
    void Start()
    {
        rawImage.enabled = false;
        audioSource.volume = PlayerPrefs.GetFloat("volume");
        //Lectura de json
        listaGestos = JsonUtility.FromJson<ListaGestos>(gestosJSON.text);
        numeroCancion = PlayerPrefs.GetInt("cancionBaile");
        tiempoValidez = PlayerPrefs.GetFloat("tiempoValidez");
        //Se obtiene el animador
        animator = GetComponent<Animator>();

        switch(numeroCancion){
            case 1:
                nombreCancion.text = "Canción 1";
                audioSource.clip = sound1;
                break;
            case 2:
                nombreCancion.text = "Canción 2";
                audioSource.clip = sound2;
                break;
            case 3:
                nombreCancion.text = "Canción 3";
                audioSource.clip = sound3;
                break;
            case 4:
                nombreCancion.text = "Canción 4";
                audioSource.clip = sound4;
                break;
            case 5:
                nombreCancion.text = "Canción 5";
                audioSource.clip = sound5;
                break;
            case 6:
                nombreCancion.text = "Canción 6";
                audioSource.clip = sound6;
                break;
        }
        
        audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        try{
            if(!audioSource.isPlaying && estadoActual == 0){
                if(PlayerPrefs.GetInt("puntuacionCancion"+numeroCancion.ToString())< puntuacionValor){
                    PlayerPrefs.SetInt("puntuacionCancion"+numeroCancion.ToString(), puntuacionValor);
                    PlayerPrefs.SetString("mensajeCancion", "¡NUEVA MEJOR PUNTUACIÓN");
                }else{
                    PlayerPrefs.SetString("mensajeCancion", "¡Bien hecho!");
                }
                PlayerPrefs.SetInt("puntuacionObtenida", puntuacionValor);
                IrResultadoPuntuaciones();
            }
            gesto = socket.Update();
            if(gesto == "Fist" && estadoActual == 0){
                //Para la canción
                txtPausa.text = "PAUSA";
                txtMensaje.text = "";
                estadoActual = 1;
                audioSource.Pause();

            }else if (gesto == "Fist" && estadoActual == 1){
                VolverCarruselCanciones();

            }else if (gesto == "Open" && estadoActual == 1){
                //Continua Baile
                txtPausa.text = "";
                estadoActual = 0;
                audioSource.Play();
                tiempoSiguienteGesto =  audioSource.time +tiempoValidez;
            }
            else if(estadoActual != 1){
                definirGesto();
                if(gesto == gestoARealizar){
                    animator.SetTrigger(gesto);
                    puntuacionValor += 100;
                    txtMensaje.text = "¡BIEN HECHO!";
                    txtMensaje.color = colorVerde;
                    txtPuntuacion.text = puntuacionValor.ToString();
                    tiempoSiguienteGesto =  audioSource.time +listaGestos.gesto[idGesto].tiempo;
                }else if(gesto != "NoGesto"){
                    txtMensaje.text = "¡VAMOS, TÚ PUEDES!";
                    txtMensaje.color = colorRojo;
                }
                
            }
        }catch (Exception ex)
        {
            socket.Start();
        } 
    }

    void definirGesto(){
        if(tiempoSiguienteGesto <= audioSource.time){
            rawImage.enabled = true;
            txtMensaje.text = "";
            do{
                idGesto = getSiguienteGesto();
            }while(idGesto == idGestoAnterior);
            idGestoAnterior=idGesto;
            gestoARealizar = listaGestos.gesto[idGesto].nombre;
            nombreGesto.text = gestoARealizar;
            tiempoSiguienteGesto += tiempoValidez;
            switch(gestoARealizar){
                case "Left":
                    videoPlayer.clip = imgLeft;
                    break;
                case "Right":
                    videoPlayer.clip = imgRight;
                    break;
                case "Up":
                    videoPlayer.clip = imgUp;
                    break;
                case "Down":
                    videoPlayer.clip = imgDown;
                    break;
                case "Open":
                    videoPlayer.clip = imgOpen;
                    break;
                case "Pinch":
                    videoPlayer.clip = imgPinch;
                    break;
                case "WaveIn":
                    videoPlayer.clip = imgWaveIn;
                    break;
                case "WaveOut":
                    videoPlayer.clip = imgWaveOut;
                    break;
                case "Forward":
                    videoPlayer.clip = imgForward;
                    break;
                case "Backward":
                    videoPlayer.clip = imgBackward;
                    break;   
            }
        }
        if(audioSource.time<8.0f){
            txtMensaje.color = colorVerde;
            txtMensaje.text = ((int)(8.0f-audioSource.time)).ToString();
        }else if(audioSource.time<10.0f){
            txtMensaje.color = colorVerde;
            txtMensaje.text = "¡A JUGAR!";
        }


    }
    void VolverCarruselCanciones(){
        gestorEscena.LoadNextScene(1);
    }
    void IrResultadoPuntuaciones(){
        gestorEscena.LoadNextScene(4);
    }

    int getSiguienteGesto(){
        return (int) Random.Range(1.0f, 10.0f);
    }
}
