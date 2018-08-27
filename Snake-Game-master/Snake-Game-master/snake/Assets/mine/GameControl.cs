using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameControl : MonoBehaviour {
	
	public Text scoreText, gameOverText, lifeText, winText;
	
	public int playerScore = 0;
    int life = 3;

	// Use this for add score function
	public void AddScore () {	
		playerScore++;
		string text = "Score: "+playerScore;
		scoreText.text = text;
        //if player reach 100, player win
        if(playerScore>=100){
            winText.enabled = true;
            Time.timeScale = 0;
        }
	}
	
	// Game over text
	public void GameOverText() {
		gameOverText.enabled = true;
		Time.timeScale = 0;
	}
    
    public bool LifeText() {
        life--;
        string text = "Life: "+life;
		lifeText.text = text;
		//Time.timeScale = 0;
        if(life != 0){
            return true;
        }else{
            return false;
        }
	}
    //used in spawn.cs
    public int getScore(){
        return playerScore;
    }
}
