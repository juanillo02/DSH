using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TableroJuego : MonoBehaviour
{
    public Player jugador1;
    public Player jugador2;
    public Player jugador3;
    public Player jugador4;
    static public int turno = 0;
    private int numJugadores = 4;
    static public Text puntos;
    public Text turnos;
    public Text numJugador;
    private string minijuego;
    static public bool juegoTerminado = false; // Variable para indicar si el juego ha terminado
    static public int jugador_gana = 1;
    static public int turno_juego;

    // Start is called before the first frame update
    void Start()
    {
        puntos = GameObject.FindWithTag("puntuacion").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IniciarJuego(){
        numJugadores = Menu.Jugadores;
        Alien.mensajeInicio.enabled = false;
        puntos.enabled = true;
        numJugador.enabled = true;
        turno_juego = 1;
        turnos.text = "Turno: " + turno_juego + "/10";
        turnos.enabled = true;
        jugador1.ActivarTurno();
    }

    public void SiguienteTurno(){
        turno++;
        turno = turno % numJugadores;
        if (turno == 0){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.blue;
            turno_juego++;
            if(turno_juego > 10){
                    int gana = Math.Max(jugador1.puntuacion, Math.Max(jugador2.puntuacion, Math.Max(jugador3.puntuacion, jugador4.puntuacion)));
                if (gana == jugador1.puntuacion){
                    jugador_gana = 1;
                }if (gana == jugador2.puntuacion){
                    jugador_gana = 2;
                }if (gana == jugador3.puntuacion){
                    jugador_gana = 3;
                }if (gana == jugador4.puntuacion){
                    jugador_gana = 4;
                }
                SceneManager.LoadScene("AnuncioJugador");
            }
            turnos.text = "Turno: " + turno_juego + "/10";
            jugador1.ActivarTurno();
        }
        if (turno == 1){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.magenta;
            jugador2.ActivarTurno();
        }
        if (turno == 2){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.green;
            jugador3.ActivarTurno();
        }
        if (turno == 3){
            numJugador.text = "Jugador " + (turno+1);
            numJugador.color = Color.red;
            jugador4.ActivarTurno();
        }
    }

    public void ActivarMinijuego(){

        int juego = UnityEngine.Random.Range(1, 6); //cambiar el rango cuando esten los minijuegos

        switch(juego){
            case 1:
                minijuego = "Juego_Parejas";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;
            case 2:
                minijuego = "Tetris";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;

            case 3:
                minijuego = "2048";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;
            
            case 4:

                minijuego = "ZigZag";

                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;

            case 5:
                minijuego = "Puzzle";
                SceneManager.LoadSceneAsync(minijuego, LoadSceneMode.Additive);
                break;

        }   
    }

    public void DesactivarMinijuego(){
        SceneManager.UnloadSceneAsync(minijuego);
    }


}