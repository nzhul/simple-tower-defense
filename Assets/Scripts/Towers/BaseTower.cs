using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BaseTower : MonoBehaviour {

	public float range = 10f;
	public GameObject projectilePrefab;
	public TowerState state;
	public float fireCooldown = .5f;
	float fireCooldownLeft = 0;
	public float damage = 1;
	public float radius = 0;
	public int cost = 5;

	EnemyManager eManager;

	void Start()
	{
		eManager = GameObject.FindObjectOfType<EnemyManager>();
	}

	void Update()
	{
		Dictionary<int, GameObject> enemies = eManager.AliveEnemies;
		GameObject nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach (var e in enemies)
		{
			float d = Vector3.Distance(this.transform.position, e.Value.transform.position);
			if (nearestEnemy == null || d < dist)
			{
				nearestEnemy = e.Value;
				dist = d;
			}
		}

		if (nearestEnemy == null)
		{
			state = TowerState.Idle;
			//Debug.Log("No enemies?");
			return;
		}
		else
		{
			state = TowerState.EnemyDetected;
		}

		Vector3 dir = nearestEnemy.transform.position - transform.position;

		fireCooldownLeft -= Time.deltaTime;
		if (dir.magnitude <= range)
		{
			state = TowerState.EnemyShooting;
			if (fireCooldownLeft <= 0)
			{
				fireCooldownLeft = fireCooldown;
				ShootAt(nearestEnemy);
			}
		}
		else
		{
			state = TowerState.EnemyDetected;
		}

		if (state == TowerState.EnemyShooting)
		{
			OnShoot();
		}

		OnUpdate(dir);
	}

	protected virtual void OnUpdate(params object[] values) { }

	protected virtual void OnShoot() { }

	protected virtual void ShootAt(GameObject e) { }
}



public enum TowerState
{
	Idle,
	EnemyDetected,
	EnemyShooting
}