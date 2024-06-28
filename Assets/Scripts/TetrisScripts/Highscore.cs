using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Highscore : MonoBehaviour {
	public int level, rows, points; 
	public Text highscoreText, levelText, rowText; 
	// Use this for initialization
	void Start () {
		level = 0;
		rows = 0; 
		points = 0; 
		gameObject.GetComponent<Movement>().setNewSpeed(); 
	}
	

	//Scoring system found at http://tetris.wikia.com/wiki/Scoring
	public void addPointsForLines(int lines){
		if (lines > 0) {
			switch (lines) {
			case 1:
				points += 40 * (level + 1); 
				break;
			case 2: 
				points += 100 * (level + 1); 
				break; 
			case 3: 
				points += 300 * (level + 1); 
				break;
			case 4: 
				points += 1200 * (level + 1); 
				break; 
			}
			rows += lines; 
			level = (int)rows/10; 
			gameObject.GetComponent<Movement>().setNewSpeed(); 
			transmitToUI (); 
		}
	}

	//Add points for each group which was added
	public void addPointsForCubes(){
		points += 4; 
		transmitToUI (); 
	}

	public void transmitToUI(){
		highscoreText.text = points.ToString (); 
		levelText.text = level.ToString (); 
		rowText.text = rows.ToString (); 
	}

    public void Update(){
        if(points >= 200)
        {
            ClearCubes(); // Llama a la función para limpiar los cubos
            TableroJuego.juegoTerminado = true;
            Player.victoria=true;
            Scene tablero = SceneManager.GetSceneByName("Tablero");
            SceneManager.SetActiveScene(tablero);
        }
    }

    // Función para limpiar los cubos
    private void ClearCubes(){
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach(GameObject cube in cubes){
            Destroy(cube);
        }
    }

}
