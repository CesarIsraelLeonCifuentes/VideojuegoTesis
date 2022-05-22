using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luz : MonoBehaviour
{
    //Objeto a enfocar
    public Transform targetPiso;
    //Luz
    Light luzAmbiental;
    void Start()
    {
        luzAmbiental = GetComponent<Light>();
        StartCoroutine(CabiarColorLuz());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //luzAmbiental.LookAt(targetPiso);
        
    }

    private IEnumerator CabiarColorLuz(){
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            luzAmbiental.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

    }
}
