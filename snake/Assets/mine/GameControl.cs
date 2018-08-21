using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameControl : MonoBehaviour {
	
	public Text scoreText, gameOverText;
	
	int playerScore = 0;

	// Use this for add score function
	public void AddScore () {	
		playerScore++;
		string text = "Score: "+playerScore;
		scoreText.text = text;
	}
	
	// Game over text
	public void GameOverText() {
		gameOverText.enabled = true;
		Time.timeScale = 0;
	}
}
