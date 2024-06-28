using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfazZigZag : MonoBehaviour
{
    public GameObject menuInicio;
    public bool menuMostradoInicio;

    public GameObject menuGanador;
    public bool menuMostradoGanador;

    public GameObject menuPerder;
    public bool menuMostradoPerder;

    public int segundosCronometro;
    public Text cronometro;
    private JugadorZigZag jugadorZigZag;

    public void MostrarMenuInicio()
    {
        menuInicio.SetActive(true);
        menuMostradoInicio = true;
    }

    public void EsconderMenuInicio()
    {
        menuInicio.SetActive(false);
        menuMostradoInicio = false;
        ActivarCronometro();
    }

    public void MostrarMenuGanador()
    {
        menuGanador.SetActive(true);
        menuMostradoGanador = true;
    }

    public void EsconderMenuGanador()
    {
        menuGanador.SetActive(false);
        menuMostradoGanador = false;
    }

    public void MostrarMenuPerder()
    {
        menuPerder.SetActive(true);
        menuMostradoPerder = true;
    }

    public void EsconderMenuPerder()
    {
        menuPerder.SetActive(false);
        menuMostradoPerder = false;
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

        if (segundosCronometro >= 60)
        {
            jugadorZigZag.GameOver();
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
        EsconderMenuGanador();
        EsconderMenuPerder();
        MostrarMenuInicio();
    }

    public void FinalizarJuego()
    { //esta funcion debe usarse para cerrar todos los minijuegos
        Scene tablero = SceneManager.GetSceneByName("Tablero");
        SceneManager.SetActiveScene(tablero);
        
    }

    public void PerderJuego()
    { //esta funcion debe usarse para cerrar todos los minijuegos
        Scene tablero = SceneManager.GetSceneByName("Tablero");
        SceneManager.SetActiveScene(tablero);
        
    }
}
