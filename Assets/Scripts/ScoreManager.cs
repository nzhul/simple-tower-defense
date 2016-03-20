using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private int lives = 20;
	private int money = 265;
	private int currentWave = 1;
	private int wavesCount = 7;
	private string enemyName;
	private int currentEnemy;
	private int enemiesCount;
	private int nextEnemyCount;
	private string nextEnemyName;

	public int Money
	{
		get
		{
			return money;
		}
		set
		{
			this.money = value;
			moneyText.text = "Gold: " + money.ToString();
		}
	}

	public int Lives
	{
		get { return lives; }
		set
		{
			this.lives = value;
			livesText.text = "Lives: " + lives.ToString();
		}
	}

	public int CurrentWave
	{
		get { return currentWave; }
		set
		{
			this.currentWave = value;
			wavesText.text = "Wave: " + currentWave.ToString() + "/" + wavesCount.ToString();
		}
	}

	public int CurrenyEnemy
	{
		get { return currentEnemy; }
		set
		{
			this.currentEnemy = value;
			UpdateEnemiesSpawningText();
		}
	}

	public int EnemiesCount
	{
		get { return enemiesCount; }
		set
		{
			this.enemiesCount = value;
			UpdateEnemiesSpawningText();
		}
	}

	public string EnemyName
	{
		get { return enemyName; }
		set
		{
			this.enemyName = value;
			UpdateEnemiesSpawningText();
		}
	}

	public int NextEnemiesCount
	{
		get { return nextEnemyCount; }
		set
		{
			this.nextEnemyCount = value;
			UpdateNextEnemiesText();
		}
	}

	public string NextEnemyName
	{
		get { return nextEnemyName; }
		set
		{
			this.nextEnemyName = value;
			UpdateNextEnemiesText();
		}
	}

	private void UpdateNextEnemiesText()
	{
		nextEnemyText.text = "Next: " + nextEnemyCount.ToString() + " " + nextEnemyName + "s";
	}

	private void UpdateEnemiesSpawningText()
	{
		enemiesText.text = "Spawning: " + currentEnemy.ToString() + "/" + enemiesCount.ToString() + " " + enemyName;
	}

	public int WavesCount
	{
		get { return wavesCount; }
		set
		{
			this.wavesCount = value;
			wavesText.text = "Wave: " + currentWave.ToString() + "/" + wavesCount.ToString();
		}
	}

	public Text moneyText;
	public Text livesText;
	public Text wavesText;
	public Text enemiesText;
	public Text nextEnemyText;

	void Start()
	{
		this.Money = 265;
		this.Lives = 20;
	}
	
	public void LoseLife(int l = 1)
	{
		this.Lives -= l;

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
