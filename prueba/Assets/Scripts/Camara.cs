using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Vector2 sensibility;
    private new Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("Main Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Mouse X");
        float Vertical = Input.GetAxis("Mouse Y");

        if(Horizontal != 0)
        {
            transform.Rotate(Vector3.up * Horizontal * sensibility.x);
        }
        if(Vertical != 0)
        {
            //transform.Rotate(Vector3.left * Vertical * sensibility.y);
            float angulo = (camera.localEulerAngles.x - Vertical * sensibility.y + 360) % 360;
            if(angulo > 180)
            {
                angulo -= 360;
            }
            angulo = Mathf.Clamp(angulo, -80, 80);
            camera.localEulerAngles = Vector3.right * angulo;
        }
    }
}
