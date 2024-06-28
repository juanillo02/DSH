using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class CrearCartas : MonoBehaviour
{
    public GameObject CartaPrefab;
    public int Ancho; //Dimensiones del tablero
    public Transform CartasParent;
    private List<GameObject> Cartas = new List<GameObject>();

    public Material[] materiales;
    public Texture2D[] texturas;

    public int contadorClicks;
    public Text textoContadorIntentos;

    public Carta CartaMostrada;
    public bool sePuedeMostrar = true;

    public int numParejasEncontradas;
    public InterfazUsuario interfazUsuario;
    
    public void Crear()
    {
        int cont = 0;
        for (int i = 0; i < Ancho; i++)
        {
            for (int x = 0; x < Ancho; x++)
            {
                GameObject CartaTemp = Instantiate(CartaPrefab, new UnityEngine.Vector3(x, 0, i), UnityEngine.Quaternion.Euler(0, 180, 0));
                
                Cartas.Add(CartaTemp);

                CartaTemp.GetComponent<Carta>().PosicionOriginal = new UnityEngine.Vector3(x, 0, i);
                CartaTemp.GetComponent<Carta>().IdCarta = cont;

                CartaTemp.transform.parent = CartasParent; //Guarda todos los clones de cartas en un gameobject vacio

                cont ++;
            }
        }
        AsigarTexturas();
        Barajar();
    }

    public void AsigarTexturas() //Controlar que se crean de 2 en 2
    {
        for(int i = 0; i < Cartas.Count; i++)
        {
            Cartas[i].GetComponent<Carta>().AsignarTextura(texturas[i/2]);
        }
    }

    void Barajar() //Las movemos aleatoriamente por el tablero
    {
        int aleatorio;
        for (int i = 0; i < Cartas.Count; i++)
        {
            aleatorio = Random.Range(i, Cartas.Count);
            Cartas[i].transform.position = Cartas[aleatorio].transform.position;
            Cartas[aleatorio].transform.position = Cartas[i].GetComponent<Carta>().PosicionOriginal;

            Cartas[i].GetComponent<Carta>().PosicionOriginal = Cartas[i].transform.position;
            Cartas[aleatorio].GetComponent<Carta>().PosicionOriginal = Cartas[aleatorio].transform.position;
            // cartasTemp.RemoveAt(aleatorio);

        }

    }

    public void HacerClick(Carta _carta)
{
    if(CartaMostrada == null)
    {
        CartaMostrada = _carta;
    }
    else
    {
        contadorClicks++;
        ActualizarUI();

        // Verificar si se encontró una pareja
        if(CompararCartas(CartaMostrada.gameObject, _carta.gameObject))
        {
            print("Enhorabuena!");
            numParejasEncontradas++;

            // Si se encontraron todas las parejas, mostrar el menú ganador
            if(numParejasEncontradas == Cartas.Count/2)
            {
                print("oleole");
                interfazUsuario.MostrarMenuGanador();
            }
        }
        else
        {
            _carta.EsconderCarta();
            CartaMostrada.EsconderCarta();
        }

        CartaMostrada = null;

        // Verificar si se superó el número de intentos permitidos
        if(contadorClicks >= 25)
        {
            interfazUsuario.MostrarMenuPerdedor();
        }
    }  
}


    public bool CompararCartas(GameObject carta1, GameObject carta2)
    {
        bool resultado;
        if(carta1.GetComponent<MeshRenderer>().material.mainTexture == carta2.GetComponent<MeshRenderer>().material.mainTexture)
            resultado = true; 
        else
            resultado = false;
        return resultado;
    }

    public void ActualizarUI()
    {
        textoContadorIntentos.text = "Intentos: " + contadorClicks;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Crear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
