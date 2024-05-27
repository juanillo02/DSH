using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador2 : MonoBehaviour
{
    //private new Rigidbody rigidbody;
    public Rigidbody rb;
    public float thrust = 5;
    public bool m_isGrounded;
    public float movementSpeed;
    public Vector2 sensitivity;
    public new Transform camera;
    public float gravedad = -15f;

    public Joystick joystickGiro;
    float GiroV, GiroH;
    public float speedGiro = 1.0f;

    public Transform objetoOriginal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Giro();
        UpdateMovement();
        UpdateMouseLook();
    }

    private void UpdateMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;

            velocity = direction * movementSpeed;
        }
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; } else
            if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }

            camera.localEulerAngles = rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Movimiento") || collision.gameObject.CompareTag("Movimiento2"))
        {
            m_isGrounded = true;
            if(collision.gameObject.CompareTag("Movimiento"))
            {
                thrust = 5;
            }
            else
            {
                if(collision.gameObject.CompareTag("Movimiento2"))
                {
                    thrust *= 3;
                }
            }
            Vector3 direccionMovimiento = objetoOriginal.forward;
            float velocidad = collision.gameObject.GetComponent<Desplazo>().velocidad;
            transform.position += direccionMovimiento * velocidad * Time.deltaTime;

        }
        if(collision.gameObject.CompareTag("Salto"))
        {
            m_isGrounded = true;
            thrust = 5;
        }
        if(collision.gameObject.CompareTag("Salto2"))
        {
            m_isGrounded = true;
            thrust *= 3;
        }
        if(collision.gameObject.CompareTag("Escena"))
        {
            SceneManager.LoadScene("Tercero");
        }
    }

    void Giro()
    {
        GiroH = joystickGiro.Horizontal * speedGiro;
        GiroV = joystickGiro.Vertical * speedGiro;
        camera.Rotate(-GiroV, 0, 0);
        camera.Rotate(0, GiroH, 0);
    }
}
