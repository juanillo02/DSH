using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void Inicio()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SeleccionNivel()
    {
        SceneManager.LoadScene("Niveles");
    }

    public void Nivel1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Nivel2()
    {
        SceneManager.LoadScene("Nivel2");
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
