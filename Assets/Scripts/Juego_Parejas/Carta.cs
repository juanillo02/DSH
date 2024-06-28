using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{   
    public Material colorCarta;
    public int IdCarta = 0;
    public Vector3 PosicionOriginal;
    public Texture2D texturaAnverso;
    public Texture2D texturaReverso;

    public float TiempoDelay;
    public GameObject CrearCartas;
    public bool Mostrando;
    
    public GameObject interfazUsuario;
    
    void Awake()
    {
        CrearCartas = GameObject.Find("Scripts");   
        interfazUsuario = GameObject.Find("Scripts");
    }

    public void OnMouseDown() //Si le damos click, se da la vuelta
    {
        if (!interfazUsuario.GetComponent<InterfazUsuario>().menuMostradoInicio)
        {
            MostrarCarta();
        }
    }

    public void AsignarTextura(Texture2D _textura)
    {
        texturaAnverso = _textura;
        // colorCarta = textura;
    }

    public void MostrarCarta()
    {
        if(Mostrando == false && CrearCartas.GetComponent<CrearCartas>().sePuedeMostrar)
        {
            GetComponent<MeshRenderer>().material.mainTexture = texturaAnverso;
            // Invoke("EsconderCarta", TiempoDelay);

            Mostrando = true;
        
            CrearCartas.GetComponent<CrearCartas>().HacerClick(this);
        }
        
    }

    public void Esconder()
    {
        GetComponent<MeshRenderer>().material.mainTexture = texturaReverso;
        Mostrando = false;
        CrearCartas.GetComponent<CrearCartas>().sePuedeMostrar = true;
    }

    public void EsconderCarta()
    {
        Invoke("Esconder", TiempoDelay);
        CrearCartas.GetComponent<CrearCartas>().sePuedeMostrar = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Esconder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
