using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public float tiempo;
    private int min, sec, cent;
    private float total;
    [SerializeField] private TMP_Text textoTiempo;
    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        min = (int)(tiempo / 60f);
        sec = (int)(tiempo - min * 60f);
        cent = (int)((tiempo - (int)tiempo) * 100f);
        textoTiempo.text = string.Format("{0:00}:{1:00}:{2:00}", min, sec, cent);
        total +=tiempo;
    }
}
