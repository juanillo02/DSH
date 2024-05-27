using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CuentaAtras : MonoBehaviour
{
    //Variables publicas
    private Button boton;
    public Image imagen;
    public Sprite[] imagenesnum;
    // Start is called before the first frame update
    void Start()
    {
        //boton = GameObject.FindAnyObjectByType<Button>();  //Otra opcion de hacerlo
        boton = GameObject.FindWithTag("BotonStart").GetComponent<Button>();    //Mejor manera, se hace una etiqueta para el boton y se hace.
        boton.onClick.AddListener(Empezar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Empezar()
    {
        imagen.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);
        StartCoroutine(Contador());
        
        //SceneManager.LoadScene("Menu");
    }

    IEnumerator Contador()
    {
        for(int i = 0; i < imagenesnum.Length; i++)
        {
            imagen.sprite = imagenesnum[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }
}
