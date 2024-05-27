using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aleatorio : MonoBehaviour
{
    public GameObject[] premios;
    public int maximo;
    public Vector3 rangoSpawn = new Vector3(5.5f, 1.5f, 5.5f);
    private int totalCaptura = 0;
    // public Text Contador;
    public float radio = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        GenerarObjetosAleatorios();
    }

    void GenerarObjetosAleatorios()
    {
        int cantidadObjetos = Random.Range(1, maximo + 1); // Determina cuántos objetos se generarán

        for (int i = 0; i < cantidadObjetos; i++)
        {
            GameObject objetoAleatorio = premios[Random.Range(0, premios.Length)]; // Elige un objeto aleatorio del array

            // Genera una posición aleatoria dentro del rango de spawn del contenedor
            Vector3 posicionAleatoria = transform.position + new Vector3(Random.Range(-rangoSpawn.x, rangoSpawn.x),
                                                                          Random.Range(-rangoSpawn.y, rangoSpawn.y),
                                                                          Random.Range(-rangoSpawn.z, rangoSpawn.z));

            // Instancia el objeto en la posición aleatoria
            Instantiate(objetoAleatorio, posicionAleatoria, Quaternion.identity, transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Captura"))
        {
            totalCaptura++;
            //Contador.text = "Contador = " + totalCaptura;
            Destroy(other.gameObject);
            if(totalCaptura == 10)
            {
                SceneManager.LoadScene("Final");
            }
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radio;
            Instantiate(premios[maximo], randomPosition, Quaternion.identity);
            // maximo = Random.Range(0,premios.Length);
            // Instantiate(premios[maximo], new Vector3(Random.Range(-3.5f, 3.5f), 0.25f, Random.Range(-3.5f, 3.5f)), premios[maximo].transform.rotation);
        }
    }
}
