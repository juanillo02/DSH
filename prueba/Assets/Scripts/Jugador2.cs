using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador2 : MonoBehaviour
{
    public float thrust = 5;
    public bool m_isGrounded;
    public Transform objetoOriginal;

    // Start is called before the first frame update
    void Start()
    {
        m_isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void OnCollisionEnter(Collision collision)
    {
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
        if(collision.gameObject.CompareTag("Movimiento"))
        {
            transform.position = objetoOriginal.position;
            transform.rotation = objetoOriginal.rotation;
        }
        if(collision.gameObject.CompareTag("Escena"))
        {
            SceneManager.LoadScene("Tercero");
        }
    }
}
