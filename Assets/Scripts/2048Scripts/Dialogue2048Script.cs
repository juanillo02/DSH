using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue2048Script : MonoBehaviour
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

        
        if(index == 1)
        {
            textSpeed = 0.09f;
            delay = 0.1f;
        }
        else
        {   
            if (index == 2 )
            {
                textSpeed = 0.06f;
                delay = 0.3f;
            }
            else if (index == 3)
            {
                textSpeed = 0.08f;
                delay = 0.3f;
            }
            else
            {
                textSpeed = 0.05f;
                delay = 0.2f;
            }
           
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
