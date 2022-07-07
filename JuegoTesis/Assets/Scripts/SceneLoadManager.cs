using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void LoadNextScene(int indiceEscena)
    {

        SceneManager.LoadScene(indiceEscena);
    }
}
