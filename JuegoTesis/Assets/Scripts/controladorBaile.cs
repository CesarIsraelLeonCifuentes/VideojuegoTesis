using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Text txtPuntuacion;
    public Text nombreGesto;
    public Text nombreCancion;
    public Text txtPausa;
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
        audioSource.volume = PlayerPrefs.GetFloat("volume");
        //Lectura de json
        listaGestos = JsonUtility.FromJson<ListaGestos>(gestosJSON.text);
        numeroCancion = PlayerPrefs.GetInt("cancionBaile");
        tiempoValidez = PlayerPrefs.GetFloat("tiempoValidez");
        //Se obtiene el animador
        animator = GetComponent<Animator>();
        //Escuchando entrada
        socket.Start();

        switch(numeroCancion){
            case 1:
                nombreCancion.text = "Canción 1";
                break;
            case 2:
                nombreCancion.text = "Canción 2";
                break;
            case 3:
                nombreCancion.text = "Canción 3";
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying && estadoActual == 0){
            PlayerPrefs.SetInt("puntuacionCancion"+numeroCancion.ToString(), puntuacionValor);
            VolverCarruselCanciones();
        }
        gesto = socket.Update();
        if(gesto == "Fist" && estadoActual == 0){
            //Para la canción
            txtPausa.text = "PAUSA";
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
                txtPuntuacion.text = puntuacionValor.ToString();
                tiempoSiguienteGesto =  audioSource.time +listaGestos.gesto[idGesto].tiempo;
            }
            
        } 
    }

    void definirGesto(){
        if(tiempoSiguienteGesto <= audioSource.time){
            do{
                idGesto = getSiguienteGesto();
            }while(idGesto == idGestoAnterior);
            idGestoAnterior=idGesto;
            gestoARealizar = listaGestos.gesto[idGesto].nombre;
            nombreGesto.text = gestoARealizar;
            tiempoSiguienteGesto += tiempoValidez;
        }
    }
    void VolverCarruselCanciones(){
        gestorEscena.LoadNextScene(1);
    }

    int getSiguienteGesto(){
        return (int) Random.Range(1.0f, 8.0f);
    }
}
