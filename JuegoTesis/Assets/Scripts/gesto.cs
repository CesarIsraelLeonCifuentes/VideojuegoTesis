using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gesto : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;
    public Sprite newImage1;
    public Sprite newImage2;
    bool estado = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageChange(){
        if(estado){
            image.sprite = newImage1;
        }else{
            image.sprite = newImage2;
        }
        estado = !estado;
        
    }
}
