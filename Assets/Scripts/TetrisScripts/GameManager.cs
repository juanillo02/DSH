using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startButton, window, windowPause, pauseBtn;

    DialogueTetrisScript dialogo;

    // Use this for initialization
    void Start()
    {
        dialogo = GameObject.Find("DialogueTetrisScript").GetComponent<DialogueTetrisScript>();
        startButton.GetComponent<Button>().interactable = false; // Deshabilitar el botón de inicio
        dialogo.OnDialogueEnd.AddListener(EnableStartButton); // Suscribirse al evento para habilitar el botón de inicio al final del diálogo
        dialogo.MostrarPanel(); // Mostrar el panel de diálogo
    }

    void EnableStartButton()
    {
        startButton.GetComponent<Button>().interactable = true; // Habilitar el botón de inicio cuando el diálogo haya terminado
    }

    public void OnClickStart()
    {
        Time.timeScale = 1;
        window.SetActive(false);
        this.gameObject.GetComponent<Movement>().startGame();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickRestart()
    {
        windowPause.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickPause()
    {
        pauseBtn.GetComponent<Button>().interactable = false;
        Time.timeScale = 0F;
        windowPause.SetActive(true);
    }

    public void OnClickContinue()
    {
        pauseBtn.GetComponent<Button>().interactable = true;
        Time.timeScale = 1F;
        windowPause.SetActive(false);
    }
}
