using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salto : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust = 5;
    public Jugador2 jugador;

    public void Saltar(string name)
    {
        if(name == "jug1")
        {
            if (jugador.m_isGrounded)
            {
                jugador.m_isGrounded = false;
                //rb.AddForce(0, thrust, 0, ForceMode.Impulse);
                rb.AddForce(Vector3.up * thrust, ForceMode.Impulse);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Movimiento") || collision.gameObject.CompareTag("Movimiento2"))
        {
            jugador.m_isGrounded = true;
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
            Vector3 direccionMovimiento = jugador.objetoOriginal.forward;
            // bool haciaAdelante = true;
            // Vector3 direccion = (collision.gameObject.transform.position - collision.gameObject.GetComponent<Desplazo>().puntoInicial).normalized;
            float velocidad = collision.gameObject.GetComponent<Desplazo>().velocidad;
            transform.position += direccionMovimiento * velocidad * Time.deltaTime;
            // float distancia = collision.gameObject.GetComponent<Desplazo>().distancia;
            // Vector3 nuevaPosicion = transform.position + transform.forward * velocidad * Time.deltaTime;
            // if (Vector3.Distance(direccion, nuevaPosicion) >= distancia)
            // {
            //     haciaAdelante = !haciaAdelante;
            // }
            
            // if (haciaAdelante)
            // {
            //     transform.position += transform.forward * velocidad * Time.deltaTime;
            // }
            // else
            // {
            //     transform.position -= transform.forward * velocidad * Time.deltaTime;
            // }

        }
        if(collision.gameObject.CompareTag("Salto"))
        {
            jugador.m_isGrounded = true;
            thrust = 5;
        }
        if(collision.gameObject.CompareTag("Salto2"))
        {
            jugador.m_isGrounded = true;
            thrust *= 3;
        }
        if(collision.gameObject.CompareTag("Escena"))
        {
            SceneManager.LoadScene("Tercero");
        }
    }

}
