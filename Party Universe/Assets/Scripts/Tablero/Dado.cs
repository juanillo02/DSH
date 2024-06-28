using UnityEngine;

public class Dado : MonoBehaviour
{
    private System.Random random;

    void Start()
    {
        // Inicializamos la instancia de Random para generar números aleatorios
        random = new System.Random();
    }

    public int Tirar()
    {
        // Generamos un número aleatorio entre 1 y 6, simulando una tirada de dado
        return random.Next(1, 7);
    }
}