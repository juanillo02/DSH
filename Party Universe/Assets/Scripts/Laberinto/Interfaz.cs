using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interfaz : MonoBehaviour
{
    public GameObject menuInicio;
    public bool menuMostradoInicio;

    public int segundosCronometro;
    public Text cronometro;

    public void MostrarMenuInicio()
    {
        menuInicio.SetActive(true);
        menuMostradoInicio = true;
    }

    public void EsconderMenuInicio()
    {
        menuInicio.SetActive(false);
        menuMostradoInicio = false;
        Cursor.lockState = CursorLockMode.Locked;
        ActivarCronometro();
    }
    public void ActivarCronometro()
    {
        ActualizarCronometro();
    }

    public void ReiniciarCronometro()
    {
        segundosCronometro = 0;
    }

    public void PausarCronometro()
    {
        CancelInvoke("ActualizarCronometro");
    }

    public void ActualizarCronometro()
    {
        segundosCronometro++;
        TimeSpan tiempo = new TimeSpan(0, 0, segundosCronometro);
        cronometro.text = tiempo.ToString(@"mm\:ss");

        if (segundosCronometro >= 180)
        {
            PerderJuego();
            PausarCronometro();
        }
        else
        {
        Invoke("ActualizarCronometro", 1.0f);
        }
    }

    //Cuando se pulse el boton de continuar -> Siguiente escena
    // Start is called before the first frame update
    public void Start()
    {
        MostrarMenuInicio();
        // ActivarCronometro();
    }

    public void FinalizarJuego()
    {
        //esta funcion debe usarse para cerrar todos los minijuegos
        SceneManager.LoadScene("Final");
        
    }

    public void PerderJuego()
    {
        //esta funcion debe usarse para cerrar todos los minijuegos
        SceneManager.LoadScene("FinalMalo");
        
    }
}
