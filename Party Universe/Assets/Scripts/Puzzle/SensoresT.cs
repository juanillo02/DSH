using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensoresT : MonoBehaviour
{
    public GameObject sensorLeft, sensorRight, sensorUp, sensorDown;
    public float radioSensor = 1f;
    public bool ocupadoLeft, ocupadoRight, ocupadoUp, ocupadoDown;

    void Start()
    {
        Comprobar();
    }

    void Comprobar()
    {
        ocupadoLeft = Physics2D.OverlapCircle(sensorLeft.transform.position, radioSensor);
        ocupadoRight = Physics2D.OverlapCircle(sensorRight.transform.position, radioSensor);
        ocupadoUp = Physics2D.OverlapCircle(sensorUp.transform.position, radioSensor);
        ocupadoDown = Physics2D.OverlapCircle(sensorDown.transform.position, radioSensor);
    }

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    void Update()
    {
        Comprobar();
    }
}
