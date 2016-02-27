using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int lives = 20;
	public int money = 100;

	public Text moneyText;
	public Text livesText;


	void Update()
	{
		//FIXME: This doesn't actually need to update the text every frame
		moneyText.text = "Gold: " + money.ToString();
		livesText.text = "Lives: " + lives.ToString();
	}
	
	public void LoseLife(int l = 1)
	{
		lives -= l;

		if (lives<=0)
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		Debug.Log("Game Over");
		// TODO: Send the player to a game-over screen instead!
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
