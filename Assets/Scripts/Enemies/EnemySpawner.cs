using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
	float spawnCooldown = .5f;
	float spawnCooldownRepaining = 3;
	EnemyManager eManager;

	[System.Serializable]
	public class WaveComponent
	{
		public GameObject enemyPrefab;
		public int num;
		[System.NonSerialized]
		public int spawned = 0;
	}

	public WaveComponent[] waveComps;

	// Use this for initialization
	void Start()
	{
		eManager = GameObject.FindObjectOfType<EnemyManager>();
	}

	// Update is called once per frame
	void Update()
	{
		spawnCooldownRepaining -= Time.deltaTime;
		if (spawnCooldownRepaining < 0)
		{
			spawnCooldownRepaining = spawnCooldown;
			bool didSpawn = false;

			// Go through the wave comps until we find something to spawn
			foreach (WaveComponent wc in waveComps)
			{
				if (wc.spawned < wc.num)
				{
					wc.spawned++;
					// Spawn it!
					GameObject newEnemyObject = Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation) as GameObject;
					Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
					newEnemy.enemyId = eManager.currentEnemyId;
                    eManager.AliveEnemies.Add(newEnemy.enemyId,newEnemy.gameObject);
					eManager.currentEnemyId++;
					didSpawn = true;
					break;
				}
			}

			if (didSpawn == false)
			{
				// Wave must be complete!
				// TODO: Instantiate next wave object!
				if (transform.parent.childCount > 1)
				{
					transform.parent.GetChild(1).gameObject.SetActive(true);

				}
				else
				{
					// that was the last wave -- what do we want to do ?
					// what if instead of destrying wave objects
					// we just makde them inactive, and then when we run
					// out of waves, we restart at the first one, 
					// but double all enemy HPs or something?
				}

				Destroy(gameObject);
			}
		}
	}
}
