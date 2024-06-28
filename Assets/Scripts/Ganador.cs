using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Ganador : MonoBehaviour
{
    private Animator animator;
    public Material jugador1;
    public Material jugador2;
    public Material jugador3;
    public Material jugador4;
    public GameObject ganador;
    public GameObject podio;
    public int velocidadMovimiento = 6;
    public Text anuncio;
    public Text num_jugador;
    public Text has_ganado;
    public Camera camara;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(TableroJuego.jugador_gana == 1){
            ganador.GetComponent<Renderer>().material = jugador1;
            num_jugador.text = "Jugador 1";
            num_jugador.color = Color.blue;
        }
        if(TableroJuego.jugador_gana == 2){
            ganador.GetComponent<Renderer>().material = jugador2;
            num_jugador.text = "Jugador 2";
            num_jugador.color = Color.magenta
            ;
        }
        if(TableroJuego.jugador_gana == 3){
            ganador.GetComponent<Renderer>().material = jugador3;
            num_jugador.text = "Jugador 3";
            num_jugador.color = Color.green;
        }
        if(TableroJuego.jugador_gana == 4){
            ganador.GetComponent<Renderer>().material = jugador4;

            num_jugador.text = "Jugador 4";
            num_jugador.color = Color.red;
        }

        animator.SetBool("move", true);
        animator.SetTrigger("jump");
        
        Invoke("Entrar_ganador", 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Entrar_ganador(){
        StartCoroutine(MoverJugadorCoroutine());
        anuncio.enabled = false;
        num_jugador.enabled = true;
        Invoke("AnimGanar", 3);
    }

    void AnimGanar(){
        animator.SetTrigger("victory");
        has_ganado.enabled = true;
        Invoke("ApareceAlien", 4);
    }

    void ApareceAlien(){
        StartCoroutine(MoverCamaraCoroutine());
        has_ganado.enabled = false;
        num_jugador.enabled = false;
        Invoke("CargarLaberinto",5f);

    }

    void CargarLaberinto(){
        SceneManager.LoadScene("Laberinto");
    }


    IEnumerator MoverJugadorCoroutine()
    {
        // Obtenemos la posición inicial y final del jugador
        Vector3 endPos = new Vector3(podio.transform.position.x, transform.position.y , podio.transform.position.z);

        // Mientras el jugador no haya llegado a la posición final
        while (transform.position != endPos)
        {
            // Calculamos el paso de movimiento
            float step = velocidadMovimiento * Time.deltaTime;

            // Movemos al jugador hacia la posición final de manera suave
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);

            // Esperamos al próximo frame
            yield return null;
        }

        animator.SetBool("move", false);

        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

    }

    IEnumerator MoverCamaraCoroutine()
    {
        
        // Obtenemos la posición inicial y final del jugador
        Vector3 endPos = new Vector3(camara.transform.position.x, camara.transform.position.y , camara.transform.position.z-7.5f);

        // Mientras el jugador no haya llegado a la posición final
        while (transform.position != endPos)
        {
            // Calculamos el paso de movimiento
            float step = velocidadMovimiento * Time.deltaTime;

            // Movemos al jugador hacia la posición final de manera suave
            camara.transform.position = Vector3.MoveTowards(camara.transform.position, endPos, step);

            // Esperamos al próximo frame
            yield return null;
        }

    
    }
}
