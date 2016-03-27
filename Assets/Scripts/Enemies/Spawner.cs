using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Transform[] spawnPositions;
	public Wave[] waves;
	public float nextWaveDelay = 5f;
	public float nextComponentSpawnDelay = 3f;
	public float enemySpawnCooldown = 1f;

	public int waveIndex = 0;
	int componentIndex = 0;
	float enemySpawnTimeRemaining = 3f;
	bool isFinalWave;

	EnemyManager eManager;
	ScoreManager scoreManager;

	void Start()
	{
		eManager = GameObject.FindObjectOfType<EnemyManager>();
		scoreManager = GameObject.FindObjectOfType<ScoreManager>();
		scoreManager.WavesCount = waves.Length;
		scoreManager.EnemiesCount = waves[waveIndex].waveComponents[componentIndex].enemiesCount;
		scoreManager.EnemyName = waves[waveIndex].waveComponents[componentIndex].enemyPrefab.GetComponent<Enemy>().enemyName;
		scoreManager.NextEnemyName = waves[waveIndex].waveComponents[componentIndex + 1].enemyPrefab.GetComponent<Enemy>().enemyName;
		scoreManager.NextEnemiesCount = waves[waveIndex].waveComponents[componentIndex + 1].enemiesCount;
	}

	void Update()
	{
		enemySpawnTimeRemaining -= Time.deltaTime;
		Wave currentWave = waves[waveIndex];
		WaveComponent currentWaveComponent = currentWave.waveComponents[componentIndex];

		if (enemySpawnTimeRemaining < 0 && currentWaveComponent.spawnedEnemyCount < currentWaveComponent.enemiesCount)
		{
			enemySpawnTimeRemaining = enemySpawnCooldown;

			//Debug.Log("Instantiate enemy: waveIndex: " + waveIndex + "; componentIndex: " + componentIndex);
			scoreManager.CurrenyEnemy = currentWaveComponent.spawnedEnemyCount + 1;
			scoreManager.EnemiesCount = currentWaveComponent.enemiesCount;
			if (componentIndex < currentWave.waveComponents.Length - 1)
			{
				scoreManager.NextEnemyName = currentWave.waveComponents[componentIndex + 1].enemyPrefab.GetComponent<Enemy>().enemyName;
				scoreManager.NextEnemiesCount = currentWave.waveComponents[componentIndex + 1].enemiesCount;
			}
			else
			{
				scoreManager.NextEnemyName = "n/a";
				scoreManager.NextEnemiesCount = 0;
			}

			Transform spawnTransform = GetRandomSpawnTransform();
            GameObject newEnemyObject = Instantiate(currentWaveComponent.enemyPrefab, spawnTransform.position, new Quaternion(spawnTransform.rotation.x, spawnTransform.rotation.y - 180, spawnTransform.rotation.z, spawnTransform.rotation.w)) as GameObject;
			Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
			newEnemy.enemyId = eManager.currentEnemyId;
			eManager.AliveEnemies.Add(newEnemy.enemyId, newEnemy.gameObject);
			eManager.currentEnemyId++;
			scoreManager.EnemyName = currentWaveComponent.enemyPrefab.GetComponent<Enemy>().enemyName;

			currentWaveComponent.spawnedEnemyCount++;

			if (currentWaveComponent.enemiesCount == currentWaveComponent.spawnedEnemyCount && !isFinalWave)
			{
				componentIndex++;
				enemySpawnTimeRemaining += nextComponentSpawnDelay;
			}

			if (componentIndex == currentWave.waveComponents.Length && !isFinalWave)
			{
				waveIndex++;
				scoreManager.CurrentWave = waveIndex + 1;
				componentIndex = 0;
				enemySpawnTimeRemaining += nextWaveDelay;
			}

			if (waveIndex == waves.Length - 1)
			{
				isFinalWave = true;
			}
		}
	}

	Transform GetRandomSpawnTransform()
	{
		int spawnerIndex = Random.Range(0, spawnPositions.Length);
		return spawnPositions[spawnerIndex].transform;

	}

	[System.Serializable]
	public class Wave
	{
		public WaveComponent[] waveComponents;
	}

	[System.Serializable]
	public class WaveComponent
	{
		public GameObject enemyPrefab;
		public int enemiesCount;
		public int spawnedEnemyCount;
	}
}