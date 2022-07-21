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
function verificarCantidad(msg, data){   
    $( Flokzu.getAllColumnValues( [[Productos::Cantidad]] ) ).each( 
        function(){  
            //Lanza un alert con el valor.
            alert( $(this).attr('value') ); 
            if(value<1){
            Flokzu.error([[Productos::Cantidad]], "La cantidad debe ser mayor a 0");
        }
    );   
}

Flokzu.onTableChange( [[Productos::Cantidad]] , verificarCantidad );
if(button == "Registrar fecha"){

        // Obtenemos la fecha ingresada
        var fechaIngresada = moment(Flokzu.getFieldValue( [[Fecha de entrega]] ) , "YYYY/MM/DD" );
      
       // Obtenemos la fecha actual
        var fechaActual = moment();

        // Si la fecha ingresada es mayor a la actual lanzamos error
        if(!fechaIngresada.isAfter(fechaActual)){
            Flokzu.error([[Fecha de entrega]], "La fecha debe ser posterior al dia actual");
        }
    }
}

//linkeamos la funcion verificarFecha a el clickeo de un botón
Flokzu.onAction(verificarFecha);

function registrarEnBase(msg,boton){
if(boton == "Registrar dirección"){
    var apiUrl = "https://app.flokzu.com/flokzuopnapi/api/3809ccdc104d405f5387031f81abcf9cadba6f1767fcdff3/database/orden_pedido_detalle_com"
    var valoresTabla = Flokzu.getFieldValue([[Productos]]);
    function(){
        //aqui dentro deberiamos mandar a hacer el registro
        //necesito el id del pedido, el id del producto o su codigo y la cantidad de producto 
        var request = new XMLHttpRequest();
        request.open("PUT", apiUrl);
        request.setRequestHeader('Content-Type', 'application/json');
        request.setRequestHeader('X-Api-Key', '3809ccdc104d405f5387031f81abcf9cadba6f1767fcdff3');
        request.setRequestHeader('X-Username', 'cesar.leon03@epn.edu.ec');
        request.onreadystatechange = function () {
            if (this.readyState === 4) {
                console.log('Status:', this.status);
                console.log('Headers:', this.getAllResponseHeaders());
                console.log('Body:', this.responseText);
            }
        };
        var detallePedido = {
            'id_pedido': 1,
            'codigo_producto': 'sadasd',
            'cantidad_producto': 'asdasda'
        };
        request.send(JSON.stringify(detallePedido));
    }


}
}
Flokzu.onAction(registrarEnBase);