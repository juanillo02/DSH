using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfazUsuario : MonoBehaviour
{
    public GameObject menuGanador;
    public bool menuMostradoGanador;

    public GameObject menuPerdedor;
    public bool menuMostradoPerdedor;

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
        ActivarCronometro();
    }

    public void MostrarMenuGanador()
    {
        menuGanador.SetActive(true);
        menuMostradoGanador = true;
        Player.victoria=true; //el marcador de victoria del jugador de cambia desde el minijuego 
        
    }

    public void EsconderMenuGanador()
    {
        menuGanador.SetActive(false);
        menuMostradoGanador = false;
    }

    public void MostrarMenuPerdedor()
    {
        menuPerdedor.SetActive(true);
        menuMostradoPerdedor = true;
        
    }

    public void EsconderMenuPerdedor()
    {
        menuPerdedor.SetActive(false);
        menuMostradoPerdedor = false;
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
        if (!TableroJuego.juegoTerminado) // Verifica si el juego aún no ha terminado
        {
            segundosCronometro++;
            TimeSpan tiempo = new TimeSpan(0, 0, segundosCronometro);
            cronometro.text = tiempo.ToString(@"mm\:ss");

            if (segundosCronometro >= 60)
            {
                MostrarMenuPerdedor();
                PausarCronometro();
            }
            else{
            Invoke("ActualizarCronometro", 1.0f);
            }
        }
    }

    //Cuando se pulse el boton de continuar -> Siguiente escena
    // Start is called before the first frame update
    public void Start()
    {
        EsconderMenuGanador();
        EsconderMenuPerdedor();
        MostrarMenuInicio();
        // ActivarCronometro();
    }

    public void FinalizarJuego(){ //esta funcion debe usarse para cerrar todos los minijuegos
        TableroJuego.juegoTerminado = true;
        Scene tablero = SceneManager.GetSceneByName("Tablero");
        SceneManager.SetActiveScene(tablero);
        
    }
}
