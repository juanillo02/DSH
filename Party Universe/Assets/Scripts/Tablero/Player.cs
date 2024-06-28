using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    public GameObject dado; // Variable para almacenar la instancia de Dado
    private int movimiento;
    private bool TuTurno = false;
    private int casilla = 0;
    private int newCasilla;
    private GameObject Casilla_Act;
    private GameObject Casilla_Next; 
    public float velocidadMovimiento = 8;
    public Camera camara;
    public TableroJuego tablero;
    public int puntuacion=0;
    static public bool victoria = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        // Creamos una instancia de Dado y la asignamos a la variable dado
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TuTurno){
            camara.transform.position = transform.position + new Vector3(0,1.5f,6.0f);
            camara.transform.rotation = Quaternion.Euler(0,180.0f,0);
            transform.localScale = new Vector3(1,1,1);
            Invoke("TiradaJugador", 0);
        }

        if(dado.GetComponent<Transform>().localScale.x <= 0.02f){
            dado.GetComponent<AnimationScriptSkull>().isScaling = false;
        }

        if (TableroJuego.juegoTerminado == true && tag == "jugador" + (TableroJuego.turno+1) ){
            TableroJuego.juegoTerminado = false;
            Invoke("FinMinijuego", 2.0f);
        }

    }

    void TiradaJugador(){
        //obtenemos la casilla actual y escondemos el logo
        Casilla_Act = GameObject.FindWithTag("casilla"+casilla);
        Casilla_Act.GetComponent<EsconderLogo>().Reducir();
        
        dado.GetComponent<Transform>().localScale = new Vector3(2,2,2);
        if (Input.GetButtonDown("Jump") && TuTurno)
        {
            TuTurno = false;
            dado.GetComponent<AnimationScriptSkull>().isRotating = true;
            animator.SetTrigger("jump");
            // Llamamos a la función PausarYRealizarTirada() después de la duración de la pausa
            Invoke("PausarYRealizarTirada", 2.0f);
        }
    }

    // Función para realizar la tirada después de la pausa
    void PausarYRealizarTirada()
    {
        // Llamamos al método Tirar() de la instancia dado
        movimiento = dado.GetComponent<Dado>().Tirar();
        dado.GetComponent<AnimationScriptSkull>().isRotating = false;

        // Establecemos la posición y rotación del dado según el resultado de la tirada
        switch (movimiento)
        {
            case 1:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 90, 0));
                break;
            case 2:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(90, 0, 0));
                break;
            case 4:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(270, 0, 0));
                break;
            case 5:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(180, 0, 0));
                break;
            case 6:
                dado.GetComponent<Transform>().SetPositionAndRotation(this.GetComponent<Transform>().position + new Vector3(0, 3, 0), Quaternion.Euler(0, 270, 0));
                break;
        }

        Invoke("DesaparecerDado", 3.0f);

    }

    void DesaparecerDado(){
        // Escalamos el dado para que desaparezca
        dado.transform.localScale = new Vector3(0.0002f, 0.0002f, 0.0002f);

        // Calculamos la nueva casilla
        newCasilla = (casilla + movimiento) % 10;

        // Obtenemos las casillas actual y siguiente
        Casilla_Act = GameObject.FindWithTag("casilla" + casilla);
        Casilla_Next = GameObject.FindWithTag("casilla" + newCasilla);

        // Calculamos la rotación hacia la siguiente casilla
        float rotacion = CalcularRotacion(Casilla_Act.transform.position, Casilla_Next.transform.position);

        // Actualizamos la rotación del jugador
        Quaternion nuevaRotacion = Quaternion.Euler(0f, rotacion, 0f);
        transform.rotation = nuevaRotacion;

        // Activamos la animación de vuelo
        animator.SetBool("move", true);
        animator.SetTrigger("jump");

        camara.transform.position= new Vector3(15.9857292f,18.8156891f,32.3321609f);
        camara.transform.rotation= Quaternion.Euler(33.8972664f,199.836166f,0.0f);

        Casilla_Act.GetComponent<EsconderLogo>().Aumentar();

        Invoke("MoverJugador", 1.0f);
    }

    void MoverJugador()
    {
        // Iniciamos la corrutina para mover al jugador
        StartCoroutine(MoverJugadorCoroutine());
    }

    float CalcularRotacion(Vector3 puntoA, Vector3 puntoB)
    {
        // Calculamos la diferencia en las posiciones de los ejes X y Z
        float deltaX = puntoB.x - puntoA.x;
        float deltaZ = puntoB.z - puntoA.z;

        // Usamos Atan2 para obtener el ángulo en radianes entre los dos puntos
        float radianes = Mathf.Atan2(deltaX, deltaZ);

        // Convertimos de radianes a grados y ajustamos el ángulo para que esté en el rango [0, 360]
        float grados = radianes * Mathf.Rad2Deg;
        grados = (grados + 360) % 360;

        // Devolvemos el ángulo de rotación resultante
        return grados;
    }

    IEnumerator MoverJugadorCoroutine()
    {
        // Obtenemos la posición inicial y final del jugador
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(Casilla_Next.transform.position.x, transform.position.y , Casilla_Next.transform.position.z);
        float distance = Vector3.Distance(startPos, endPos);

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

        // Una vez que el jugador llega a la nueva casilla, lo rotamos hacia (0,0,0)
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // Actualizamos la variable de casilla con la nueva casilla
        casilla = newCasilla;

        // Indicamos que el jugador ha terminado de moverse
        animator.SetBool("move", false);

        // Movemos la camara hacia el juagador de nuevo
        camara.transform.position = transform.position + new Vector3(0,1.5f,6.0f);
        camara.transform.rotation = Quaternion.Euler(0,180.0f,0);

        Invoke("TurnoJugador", 3.0f);
    }

    void TurnoJugador(){
        // Añadir opciones que hacer en el turno
        if(casilla == 3 || casilla == 8 ){
            puntuacion++;
            animator.SetTrigger("victory");
            Invoke("FinTurno", 1.0f);
        }
        if(casilla == 4 || casilla == 7){
            animator.SetTrigger("defeat");
            if (puntuacion>0){
                puntuacion--;
            }
            Invoke("FinTurno", 2.0f);
        }
        if(casilla == 1 || casilla == 6 || casilla == 9){
            tablero.ActivarMinijuego();
        }
        if (casilla == 2 || casilla == 5 || casilla == 0){
            Invoke("FinTurno", 1.0f);
        }
    }

    void FinMinijuego(){
        if (victoria == false){
            animator.SetTrigger("defeat");
        }
        if (victoria == true){
            puntuacion+=2;
            victoria=false;
            animator.SetTrigger("victory");
        }
        tablero.DesactivarMinijuego();
        Invoke("FinTurno", 2.0f);
    }

    void FinTurno(){
        transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        tablero.SiguienteTurno();
    }

    public void ActivarTurno(){
        TableroJuego.puntos.text="Estrellas: "+puntuacion;
        TuTurno = true;
    }
}