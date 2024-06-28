using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MoverJugador : MonoBehaviour
{
    public float velocidad = 0.5f;
    public CharacterController cc;
    public Text ContadorAliens;
    private Interfaz Interfaz;
    public Animator jugador;
    public Renderer material;
    public Material azul;
    public Material rojo;
    public Material verde;
    public Material rosa;

    // Variables privadas
    private int aliens = 0;
    private bool Iniciar = false;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        if(TableroJuego.jugador_gana == 1)
        {
            material.material = azul;
        }
        else if (TableroJuego.jugador_gana == 2)
        {
            material.material = rosa;
        }
        else if (TableroJuego.jugador_gana == 3)
        {
            material.material = verde;
        }
        else if (TableroJuego.jugador_gana == 4)
        {
            material.material = rojo;
        }
    }
    public void IniciarJuego()
    {
        Iniciar = true;
    }

    void Update()
    {
        if(Iniciar == true)
        {
            float Vertical = Input.GetAxis("Vertical");
            float Horizontal = Input.GetAxis("Horizontal");
            if(Vertical > 0)
            {
                cc.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }
            if (Vertical < 0)
            {
                cc.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
            }
            if(Horizontal > 0)
            {
                cc.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
            }
            if(Horizontal < 0)
            {
                cc.transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
            }
            Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
            cc.Move(movement * velocidad);
        }
    }
    
    void Awake()
    {
        Interfaz = GameObject.FindObjectOfType<Interfaz>();
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag =="Alien")
        {
            aliens++;
            ContadorAliens.text = "Aliens " + aliens + "/2";
            Destroy(other.gameObject);
        }
        if(aliens == 2 && other.gameObject.tag == "Cohete")
        {
            Cursor.lockState = CursorLockMode.None;
            Interfaz.FinalizarJuego();
        }
    }
}
