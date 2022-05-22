using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowScript : MonoBehaviour
{
    //Etiqueta del objeto
    public Transform targetObject;

    //Distancia entre la camara
    public Vector3 cameraOffset;
    public float distancia ;

    // Start is called before the first frame update
    void Start()
    {
        distancia = Vector3.Distance(targetObject.transform.position, transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.LookAt(targetObject);
        transform.Translate(Vector3.right * Time.deltaTime);
        cameraOffset = transform.position - targetObject.transform.position;
        Vector3 newPosition = targetObject.transform.position + cameraOffset.normalized * distancia;
        transform.position = newPosition;
    }
}
