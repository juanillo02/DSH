using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueLaberintoScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    public float delay;
    // public float fasterTextSpeedForSecondLine = 0.2f;
    // public float extraDelayForFifthLine = 2f;
    int index;

    public GameObject panel;
    public bool panelMostrado;

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {

        // Ajustar la velocidad de escritura para la segunda línea
        switch(index)
        {
            case 0:
                textSpeed = 0.06f;
                delay = 0.2f;
                break;
            case 1:
                textSpeed = 0.04f;
                delay = 0.8f;
                break;
            case 2:
                textSpeed = 0.1f;
                delay = 0.3f;
                break;
            case 3:
                textSpeed = 0.1f;
                delay = 0.2f;
                break;
            case 4:
                textSpeed = 0.04f;
                delay = 0.3f;
                break;
            case 5:
                textSpeed = 0.06f;
                delay = 0.7f;
                break;
            case 6:
                textSpeed = 0.07f;
                delay = 0.2f;
                break;
        }

        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(delay); // Esperar un poco después de escribir la línea
        NextLine();
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            EsconderPanel();
        }
    }

    public void MostrarPanel()
    {
        panel.SetActive(true);
        panelMostrado = true;

        dialogueText.text = string.Empty;
        StartDialogue();
    }

    public void EsconderPanel()
    {
        panel.SetActive(false);
        panelMostrado = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        MostrarPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
