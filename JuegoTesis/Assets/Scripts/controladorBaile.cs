using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorBaile : MonoBehaviour
{
    //Animator
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
           if(Input.GetKey("1")){
               animator.SetBool("WaveIn", true);
           }else if(Input.GetKey("2")){
               animator.SetBool("WaveOut", true);
           }else if(Input.GetKey("3")){
               animator.SetBool("Right", true);
           }else if(Input.GetKey("4")){
               animator.SetBool("Left", true);
           }else if(Input.GetKey("5")){
               animator.SetBool("Open", true);
           }else if(Input.GetKey("6")){
               animator.SetBool("Pinch", true);
           }else if(Input.GetKey("7")){
               animator.SetBool("Up", true);
           }else if(Input.GetKey("8")){
               animator.SetBool("Down", true);
           }else{
               animator.SetBool("WaveIn", false);
               animator.SetBool("WaveOut", false);
               animator.SetBool("Right", false);
               animator.SetBool("Left", false);
               animator.SetBool("Open", false);
               animator.SetBool("Pinch", false);
               animator.SetBool("Up", false);
               animator.SetBool("Down", false);
           }
    }
}
