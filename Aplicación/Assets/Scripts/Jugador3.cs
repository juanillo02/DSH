using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador3 : MonoBehaviour
{
    public GameObject[] objectsToInstantiate;
    private int n;
    private int totalCaptura = 0;
    public Text Contador;
    public float movementSpeed = 3.0f;
    private Rigidbody rb;
    public Camera camara;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = camara.transform.position;
        n = Random.Range(0,objectsToInstantiate.Length);
        Instantiate(objectsToInstantiate[n], new Vector3(Random.Range(-3.5f, 3.5f), 0.25f, Random.Range(-3.5f, 3.5f)), objectsToInstantiate[n].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
        rb.AddForce(movement * movementSpeed);

        camara.transform.position = transform.position + offset;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Captura"))
        {
            totalCaptura++;
            Contador.text = "Contador = " + totalCaptura;
            Destroy(other.gameObject);
            if(totalCaptura == 10)
            {
                SceneManager.LoadScene("Final");
            }
            n = Random.Range(0,objectsToInstantiate.Length);
            Instantiate(objectsToInstantiate[n], new Vector3(Random.Range(-3.5f, 3.5f), 0.25f, Random.Range(-3.5f, 3.5f)), objectsToInstantiate[n].transform.rotation);
        }
    }
}
