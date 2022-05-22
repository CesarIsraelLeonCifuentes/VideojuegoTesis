using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posicionJugador : MonoBehaviour
{
    // Start is called before the first frame update
     //Etiqueta del objeto
    public Transform targetObject;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         targetObject.transform.position = new Vector3(targetObject.transform.position.x,0,targetObject.transform.position.z);
    }
}
