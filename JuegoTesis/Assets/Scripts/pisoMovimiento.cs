using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pisoMovimiento : MonoBehaviour
{
    // Start is called before the first frame update
        //Etiqueta del objeto
    public Transform targetObject;

    //Distancia entre el objeto y el piso
    public Vector3 floorOffset;
    void Start()
    {
        floorOffset = transform.position - targetObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + floorOffset;
        if(floorOffset.y <(transform.position - targetObject.transform.position).y ){
            transform.position = new Vector3 (transform.position.x, targetObject.transform.position.y,transform.position.z);
        }

    }
}
