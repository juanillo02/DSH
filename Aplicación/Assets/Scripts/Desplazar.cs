using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desplazo : MonoBehaviour
{
    public float velocidad = 5f;
    public float distancia = 10f;
    public Vector3 puntoInicial;
    private bool haciaAdelante = true;
    // Start is called before the first frame update
    void Start()
    {
        puntoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nuevaPosicion = transform.position + transform.forward * velocidad * Time.deltaTime;

       
        if (Vector3.Distance(puntoInicial, nuevaPosicion) >= distancia)
        {
            haciaAdelante = !haciaAdelante;
        }
        
        if (haciaAdelante)
        {
            transform.position += transform.forward * velocidad * Time.deltaTime;
        }
        else
        {
            transform.position -= transform.forward * velocidad * Time.deltaTime;
        }
    }
}
