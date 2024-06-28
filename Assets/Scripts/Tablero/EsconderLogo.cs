using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsconderLogo : MonoBehaviour
{
    public GameObject logo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reducir(){
        logo.GetComponent<Transform>().localScale = new Vector3(0.002f, 0.002f, 0.002f);
        logo.GetComponent<Light>().range = 0;
        
    }

    public void Aumentar(){
        if (logo.tag == "skull"){
            logo.GetComponent<Transform>().localScale = new Vector3(0.3f,0.3f,0.3f);
        }
        if (logo.tag == "controller"){
            logo.GetComponent<Transform>().localScale = new Vector3(10.0f,10.0f,10.0f);
        }
        if (logo.tag == "star"){
            logo.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        }
        logo.GetComponent<Light>().range = 5.0f;
    }

}
