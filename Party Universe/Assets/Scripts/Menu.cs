using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    static public int Jugadores = 1;
    public void MenuJugadores()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Inicio(int n)
    {
        Jugadores = n;
        SceneManager.LoadScene("Tablero");
    }
    
}
