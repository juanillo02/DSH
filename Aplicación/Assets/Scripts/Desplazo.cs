using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desplazar : MonoBehaviour
{
    public float velocidad = 3f;
    public float distancia = 5f;

    private bool haciaAdelante = true;
    private Vector3 puntoInicial;
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
