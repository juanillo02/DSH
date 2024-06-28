using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleT : MonoBehaviour
{
    public List<Sprite> fichaImg  = new List<Sprite>();
    public GameObject fichaPrfb;
    public GameObject bordePrfb;
    public Sprite fichaEscondidaImg;
    public GameObject textoGanador;


    GameObject fichaEscondida;
    int numCostado;
    Vector2 posFichaEscondida;
    GameObject padreFichas;
    GameObject padreBordes;
    List<Vector3> posicionesIniciales = new List<Vector3>();
    GameObject[] _fichas;

    UI_T ui;

    void Awake()
    {
        padreFichas = GameObject.Find("Fichas");
        padreBordes = GameObject.Find("Bordes");
        ui = GameObject.FindObjectOfType<UI_T>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Mathf.Sqrt(fichaImg.Count) == Mathf.Round(Mathf.Sqrt(fichaImg.Count)))
        {
            CrearFichas();
        }
        else
        {
            print("Imposible crear fichas");
        }
    }

    // Update is called once per frame
    void CrearFichas()
    {
        int contador = 0;
        numCostado = (int) Mathf.Sqrt(fichaImg.Count);
        for(int alto  = numCostado + 2; alto > 0; alto--)
        {
            for(int ancho = 0; ancho < numCostado + 2; ancho++)
            {
                Vector3 posicion = new Vector3(ancho - (numCostado/2), alto-(numCostado/2), 0);
                if(alto == 1 || ancho == 0 || ancho == numCostado + 1 || alto == numCostado + 2)
                {
                    GameObject borde = Instantiate(bordePrfb, posicion, Quaternion.identity);
                    borde.transform.parent = padreBordes.transform;
                }
                else
                {
                    GameObject ficha = Instantiate(fichaPrfb, posicion, Quaternion.identity);
                    ficha.GetComponent<SpriteRenderer>().sprite = fichaImg[contador];
                    ficha.transform.parent = padreFichas.transform;
                    ficha.name = fichaImg[contador].name;

                    if(ficha.name == fichaEscondidaImg.name)
                    {
                        fichaEscondida = ficha;
                    }
                    contador++;
                    
                }
            }
        }
        fichaEscondida.gameObject.SetActive(false);
        _fichas = GameObject.FindGameObjectsWithTag("Ficha");
        for(int i = 0; i < _fichas.Length; i++)
        {
            posicionesIniciales.Add(_fichas[i].transform.position);
        }

        Barajar();
    }

    void Barajar()
    {
        int aleatorio = 1;
        for(int i = 0; i < _fichas.Length; i++)
        {
            aleatorio = UnityEngine.Random.Range(i, _fichas.Length);
            Vector3 posTemp = _fichas[i].transform.position;
            _fichas[i].transform.position = _fichas[aleatorio].transform.position;
            _fichas[aleatorio].transform.position = posTemp;
        }

            // PAra probar si ganas -> Inicias, le das a reiniciar y mueve un pieza
            // Vector3 posTemp = _fichas[0].transform.position;
            // _fichas[0].transform.position = _fichas[aleatorio].transform.position;
            // _fichas[aleatorio].transform.position = posTemp;
        
    }


    public void ComprobarGanador()
    {
        Debug.Log("hola");
        for(int i = 0; i < _fichas.Length; i++)
        {
            if(posicionesIniciales[i] != _fichas[i].transform.position)
                return;
        }
        fichaEscondida.gameObject.SetActive(true);
        
        ui.MostrarMenuGanador();
        // textoGanador.gameObject.SetActive(true);
    }

    public void reiniciar()
    {
        Barajar();
    }
}
