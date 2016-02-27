using UnityEngine;
using System.Collections;
using System;

public class Tower : MonoBehaviour {

	Transform turretTransform;

	public float range = 10f;
	public GameObject bulletPrefab;

	float fireCooldown = .5f;
	float fireCooldownLeft = 0;

	public float damage = 1;
	public float radius = 0;

	public int cost = 5;

	void Start()
	{
		turretTransform = transform.Find("Head");
	}

	void Update()
	{
		// TODO: Optimize this!
		Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
		Enemy nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach (Enemy e in enemies)
		{
			float d = Vector3.Distance(this.transform.position, e.transform.position);
			if (nearestEnemy == null || d < dist)
			{
				nearestEnemy = e;
				dist = d;
			}
		}

		if (nearestEnemy == null)
		{
			Debug.Log("No enemies?");
			return;
		}

		Vector3 dir = nearestEnemy.transform.position - transform.position;

		Quaternion lookRot = Quaternion.LookRotation(dir);

		turretTransform.rotation = Quaternion.Euler(270, lookRot.eulerAngles.y ,0);

		fireCooldownLeft -= Time.deltaTime;
		if (fireCooldownLeft <= 0 && dir.magnitude <= range)
		{
			fireCooldownLeft = fireCooldown;
			ShootAt(nearestEnemy);
		}
	}

	private void ShootAt(Enemy e)
	{
		// TODO: fire out the tip!
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
	}
}
