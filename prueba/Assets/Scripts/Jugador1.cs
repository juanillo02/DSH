using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    //private new Rigidbody rigidbody;
    private int totalEstrellas = 0;
    // public float movementSpeed = 100.0f;
    // public Vector2 sensitivity;
    // public new Transform camera;
    public Text Contador;

    // public Transform player;
    // public Joystick joystickMovimiento;
    // float MovimientoV, MovimientoH;
    // Vector3 movimiento;
    // public CharacterController controller;

    // public Joystick joystickGiro;
    // float GiroV, GiroH;
    // public float speedGiro = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        //camera = Camera.main.transform;
        
    }

    // void Movimiento()
    // {
    //     MovimientoH = joystickMovimiento.Horizontal + Input.GetAxis("Horizontal");
    //     MovimientoV = joystickMovimiento.Vertical + Input.GetAxis("Vertical");
    //     movimiento = player.right * MovimientoH + player.forward * MovimientoV;
    //     controller.Move(movimiento * movementSpeed * Time.deltaTime);
    // }

    // void Giro()
    // {
    //     GiroH = joystickGiro.Horizontal * speedGiro;
    //     GiroV = joystickGiro.Vertical * speedGiro;
    //     camera.Rotate(-GiroV, 0, 0);
    //     camera.Rotate(0, GiroH, 0);
    // }
    
    // private void UpdateMovement()
    // {
    //     float hor = Input.GetAxisRaw("Horizontal");
    //     float ver = Input.GetAxisRaw("Vertical");

    //     Vector3 velocity = Vector3.zero;

    //     if (hor != 0 || ver != 0)
    //     {
    //         Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;

    //         velocity = direction * movementSpeed;
    //     }

    //     velocity.y = rigidbody.velocity.y;
    //     rigidbody.velocity = velocity;
    // }

    // private void UpdateMouseLook()
    // {
    //     float hor = Input.GetAxis("Mouse X");
    //     float ver = Input.GetAxis("Mouse Y");

    //     if (hor != 0)
    //     {
    //         transform.Rotate(0, hor * sensitivity.x, 0);
    //     }

    //     if (ver != 0)
    //     {
    //         Vector3 rotation = camera.localEulerAngles;
    //         rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
    //         if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; } else
    //         if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }

    //         camera.localEulerAngles = rotation;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        //Movimiento();
        // Giro();
        // UpdateMovement();
        // UpdateMouseLook();
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Premio"))
        {
            totalEstrellas++;
            Contador.text = "Contador = " + totalEstrellas;
            Destroy(other.gameObject);
            if(totalEstrellas == 10)
            {
                SceneManager.LoadScene("Segundo");
            }
        }
    }
}
