using System.Collections;
//using OpenCover.Framework.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2048 : MonoBehaviour
{
    public static Game2048 Instance { get; private set; }

    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    private int score;
    public int Score => score;
    public GameObject startButton, window, camera;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void OnClickStart()
    {
		Time.timeScale = 1; 
		window.SetActive (false); 
		Start();
	}

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        // reset score
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();

        // hide game over screen
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }


    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return StartCoroutine(Fade(gameOver, 1f, 1f)); // Espera a que termine la animación de fade

        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de cambiar de escena

        Destroy(camera);
        TableroJuego.juegoTerminado = true;
        Scene tablero = SceneManager.GetSceneByName("Tablero");
        SceneManager.SetActiveScene(tablero);
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore) {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

	public void Update(){
		if(score >= 1000)
		{
            Destroy(camera);
            TableroJuego.juegoTerminado = true;
            Player.victoria=true;
            Scene tablero = SceneManager.GetSceneByName("Tablero");
            SceneManager.SetActiveScene(tablero);
		}
	}
}
