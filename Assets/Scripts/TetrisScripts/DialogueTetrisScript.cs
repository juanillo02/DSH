using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTetrisScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    public float delay;
    int index;

    public GameObject panel;
    public bool panelMostrado;

    public UnityEvent OnDialogueEnd; // Evento para indicar que el diálogo ha terminado

    void Start()
    {
        if (OnDialogueEnd == null)
        {
            OnDialogueEnd = new UnityEvent();
        }

        MostrarPanel(); // Mostrar el panel de diálogo al inicio
        StartDialogue(); // Iniciar el diálogo
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {

        switch(index)
        {
            case 0:
                textSpeed = 0.1f;
                delay = 0.2f;
                break;
            case 1:
                textSpeed = 0.06f; //0.04
                delay = 0.5f;
                break;
            case 2:
                textSpeed = 0.08f;
                delay = 0.3f;
                break;
            case 3:
                textSpeed = 0.07f;
                delay = 0.2f;
                break;
            case 4:
                textSpeed = 0.07f;
                delay = 0.3f;
                break;
        }

        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(delay);

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
    }

    public void EsconderPanel()
    {
        panel.SetActive(false);
        panelMostrado = false;
        OnDialogueEnd.Invoke(); // Invocar el evento cuando el diálogo ha terminado
    }
}
