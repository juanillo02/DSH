using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FinalMalo : MonoBehaviour
{
    public GameObject cristal;
    public Camera camara;
    private float velocidadMovimiento = 8;
    public Text has_perdido;
    public Text perdedor;
    public GameObject AlienAzul;
    public GameObject AlienVerde;
    private Animator animatorAzul;
    private Animator animatorVerde;
    public VideoPlayer Creditos;
    // Start is called before the first frame update
    void Start()
    {
        animatorAzul = AlienAzul.GetComponent<Animator>();
        animatorVerde = AlienVerde.GetComponent<Animator>();
        CambioAnimacion("AlienVictoria");
        Invoke("TerminaAnimacion",3f);
        Invoke("VuelaCohete",5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CambioAnimacion(string nombreAnimacion)
    {
        // Si el componente Animator no está asignado, salir del método
        if (animatorAzul == null)
        {
            Debug.LogWarning("El componente Animator no está asignado.");
            return;
        }

        if (animatorVerde == null)
        {
            Debug.LogWarning("El componente Animator no está asignado.");
            return;
        }

        // Cambiar a la animación especificada por nombre
        animatorAzul.Play(nombreAnimacion);
        animatorVerde.Play(nombreAnimacion);
    }

    void TerminaAnimacion()
    {
        CambioAnimacion("rig|SpaceGuyPose_Standing");
    }

    void VuelaCohete(){
        StartCoroutine(VueloCohete());
        has_perdido.enabled = true;
        perdedor.text="Jugador " + TableroJuego.jugador_gana;
        switch(TableroJuego.jugador_gana){
            case 1:
                perdedor.color= Color.blue;
            break;
            case 2:
                perdedor.color= Color.magenta;
            break;
            case 3:
                perdedor.color= Color.green;
            break;
            case 4:
                perdedor.color= Color.red;
            break;
        }
        
        perdedor.enabled = true;
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

         // Obtenemos la posición inicial y final del jugador
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(0, 0, 3000);
        
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
    }
}
