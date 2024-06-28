using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Final : MonoBehaviour
{
    public GameObject cristal;
    public Camera camara;
    private float velocidadMovimiento = 8;
    public Text has_ganado;
    public Text ganador;
    public Material jugador1;
    public Material jugador2;
    public Material jugador3;
    public Material jugador4;
    public GameObject jugador;
    public VideoPlayer Creditos;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("VuelaCohete",7.5f);
        
        if(TableroJuego.jugador_gana == 1){
            jugador.GetComponent<Renderer>().material = jugador1;
        }
        if(TableroJuego.jugador_gana == 2){
            jugador.GetComponent<Renderer>().material = jugador2;
            ;
        }
        if(TableroJuego.jugador_gana == 3){
            jugador.GetComponent<Renderer>().material = jugador3;
        }
        if(TableroJuego.jugador_gana == 4){
            jugador.GetComponent<Renderer>().material = jugador4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VuelaCohete(){
        StartCoroutine(VueloCohete());
        has_ganado.enabled = true;
        ganador.text="Jugador " + TableroJuego.jugador_gana;
        switch(TableroJuego.jugador_gana){
            case 1:
                ganador.color= Color.blue;
            break;
            case 2:
                ganador.color= Color.magenta;
            break;
            case 3:
                ganador.color= Color.green;
            break;
            case 4:
                ganador.color= Color.red;
            break;
        }
        
        ganador.enabled = true;
        Invoke("MostrarCreditos",5f);
    }

    void MostrarCreditos()
    {
        Creditos.enabled = true;
        Creditos.Play();
    }

    IEnumerator VueloCohete(){
        cristal.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
        camara.transform.position = new Vector3(56.3850136f,62.945385f,-29.4771271f);
        camara.transform.rotation = Quaternion.Euler(30.1846447f,326.198151f,359.99173f);

         // Obtenemos la posici贸n inicial y final del jugador
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, 0, 3000);
        
        // Mientras el jugador no haya llegado a la posici贸n final
        while (transform.position != endPos)
        {
            // Calculamos el paso de movimiento
            float step = velocidadMovimiento * Time.deltaTime;

            // Movemos al jugador hacia la posici贸n final de manera suave
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);

            // Esperamos al pr贸ximo frame
            yield return null;
        }
    }
}
