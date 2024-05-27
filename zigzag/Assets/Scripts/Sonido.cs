using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    public AudioSource quienEmite;
    public AudioClip elSonido;
    public float Volumen = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        quienEmite.PlayOneShot(elSonido, Volumen);
    }
}
