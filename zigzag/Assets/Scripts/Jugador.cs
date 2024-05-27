using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    //Variables publicas
    public Camera camara;
    public GameObject suelo;
    public float velocidad = 5.0f;
    public Rigidbody rb;
    

    public GameObject chupa_chups;
    public GameObject milka;
    public GameObject chicle_melon;
    public GameObject fresquito;

    public AudioSource quienEmite;
    public AudioClip elSonido;
    public float Volumen = 1f;
    public Text Puntos;

    //Variables privadas
    private Vector3 offSet;
    private float ValorX, ValorZ;
    private int TotalPuntos = 0;
    private bool izquierda, derecha;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offSet = camara.transform.position;
        CrearSueloInicial();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
        rb.AddForce(movement * velocidad);
        camara.transform.position = transform.position + offSet;
    }

    void CrearSueloInicial()
    {
        for(int i = 0; i < 3; i++)
        {
            ValorZ += 6.0f;
            Instantiate(suelo, new Vector3(ValorX, 0, ValorZ), Quaternion.identity);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Suelo")
        {
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Reset"))
        {
            SceneManager.LoadScene("GameOver");
        }
        if(other.gameObject.CompareTag("Premio"))
        {
            quienEmite.PlayOneShot(elSonido, Volumen);
            TotalPuntos++;
            Puntos.text = "Chuches = " + TotalPuntos + "/7";
            Destroy(other.gameObject);
            if(TotalPuntos == 7)
            {
                SceneManager.LoadScene("Nivel2");
            }
        }
        if(other.gameObject.CompareTag("Premio2"))
        {
            TotalPuntos--;
            Puntos.text = "Chuches = " + TotalPuntos + "/7";
            Destroy(other.gameObject);
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f,1.5f);
        if(aleatorio > 1.0 && derecha)
        {
            derecha = true;
            izquierda = false;
            ValorX += 6.0f;
        }
        else
        {
            if(aleatorio > 0.5 && izquierda)
            {
                derecha = false;
                izquierda = true;
                ValorX -= 6.0f;
            }
            else
            {
                derecha = true;
                izquierda = true;
                ValorZ += 6.0f;
            }
        }
        
        bool tocado = true;
        float probabilidad = Random.Range(0.0f, 1.0f);
        if(probabilidad >= 0.8 && tocado)
        {
            tocado = false;
            Quaternion rotacionTumbada = Quaternion.Euler(90, 0, 0);
            Instantiate(chicle_melon, new Vector3(ValorX, 1.5f, ValorZ), rotacionTumbada);
        }
        if(probabilidad >= 0.2 && probabilidad < 0.4 && tocado)
        {
            tocado = false;
            Vector3 posicion = new Vector3(ValorX, 1.5f, ValorZ); // Posición original
            posicion.y -= 0.5f; // Restamos 0.5 unidades en el eje Y para bajarlo
            Instantiate(chupa_chups, posicion, Quaternion.identity);
        }
        if(probabilidad >= 0.4 && probabilidad < 0.6 && tocado)
        {
            tocado = false;
            Instantiate(fresquito, new Vector3(ValorX, 1.5f, ValorZ), Quaternion.identity);
        }
        if(probabilidad >= 0.6 && probabilidad < 0.8 && tocado)
        {
            tocado = false;
            Instantiate(milka, new Vector3(ValorX, 1.5f, ValorZ), Quaternion.identity);
        }
    

        Instantiate(suelo, new Vector3(ValorX, 0, ValorZ), Quaternion.identity);
        yield return new WaitForSeconds(3);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(3);
        Destroy(suelo);        
    }

}