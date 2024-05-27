using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rotacion : MonoBehaviour
{
    public float speed = 30.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }
}
