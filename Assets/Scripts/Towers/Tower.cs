using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

	Transform turretTransform;
	public Transform muzzle1;
	public Transform muzzle2;

	public float range = 10f;
	public GameObject bulletPrefab;
	public TowerState state;

	public float fireCooldown = .5f;
	float fireCooldownLeft = 0;

	public float damage = 1;
	public float radius = 0;

	public int cost = 5;

	public int muzzleToShoot = 1;

	EnemyManager eManager;

	void Start()
	{
		turretTransform = transform.Find("head");
		eManager = GameObject.FindObjectOfType<EnemyManager>();
	}

	void Update()
	{
		// TODO: Optimize this!
		//Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
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

		Quaternion lookRot = Quaternion.LookRotation(dir);

		turretTransform.rotation = Quaternion.Euler(270, lookRot.eulerAngles.y - 180 ,0);

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
			muzzle1.transform.Rotate(0, 0, 400 * Time.deltaTime);
			muzzle2.transform.Rotate(0, 0, 400 * Time.deltaTime);
		}
	}

	private void ShootAt(GameObject e)
	{
		// TODO: fire out the tip!
		
		GameObject bullet1 = (GameObject)Instantiate(bulletPrefab, muzzle1.transform.position, transform.rotation);
		Bullet b1 = bullet1.GetComponent<Bullet>();
		b1.target = e.transform;
		b1.damage = damage;
		b1.radius = radius;

		GameObject bullet2 = (GameObject)Instantiate(bulletPrefab, muzzle2.transform.position, transform.rotation);
		Bullet b2 = bullet2.GetComponent<Bullet>();
		b2.target = e.transform;
		b2.damage = damage;
		b2.radius = radius;
	}

	public enum TowerState
	{
		Idle,
		EnemyDetected,
		EnemyShooting
	}
}
