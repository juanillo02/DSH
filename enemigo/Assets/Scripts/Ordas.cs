using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordas : MonoBehaviour
{
    public ValoresEnemigos[] valoresEnemigos;
    private ValoresEnemigos enemigoActual;
    float tiempoEspera = 0.0f;
    int numHordaActual = 0;
    int enemigosporCrear = 0;
    int enemigosporMatar = 0;
    // Start is called before the first frame update
    void Start()
    {
        NextHorda();
        Movimiento.onHitEnemy += EnemigoMuerto;
    }

    void NextHorda()
    {
        if(numHordaActual < valoresEnemigos.Length)
        {
            numHordaActual ++;
            enemigoActual = valoresEnemigos[numHordaActual - 1];
            enemigosporCrear = enemigoActual.numeroEnemigos;
            enemigosporMatar = enemigoActual.numeroEnemigos;
        }
    }

    void EnemigoMuerto()
    {
        enemigosporMatar--;
        if(enemigosporMatar <= 0)
        {
            NextHorda();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemigosporCrear > 0 && tiempoEspera <= 0)
        {
            Instantiate(enemigoActual.tipoEnemigo, Vector3.zero, Quaternion.identity);
            enemigosporCrear --;
            tiempoEspera = enemigoActual.tiempoEntreEnemigos;
        }
        else
        {
            tiempoEspera -= Time.deltaTime;
        }
    }
}