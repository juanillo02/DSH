using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salida : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Salir();
        }
    }
    public void Salir()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
