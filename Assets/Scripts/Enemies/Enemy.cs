using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;
	public int enemyId = 0;
	public GameObject DeathEffectParticlePrefab;

	public float speed = 10;
	public float health = 1f;
	public int moneyValue = 10;
	private float maxHealth;

	public Image HealthBar;

	void Start()
	{
		pathGO = GameObject.Find("Path");
		maxHealth = health;
	}

	void Update()
	{
		if (targetPathNode == null)
		{
			GetNextPathNode();
			if (targetPathNode == null)
			{
				// we've run out of path!
				ReachGoal();
				return;
			}
		}

		Vector3 dir = targetPathNode.position - this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distThisFrame)
		{
			// We reached the node
			targetPathNode = null;
		}
		else
		{
			// TODO: Consider ways to smooth this motion
			// Move towards node
			transform.Translate(dir.normalized * distThisFrame, Space.World);
			Quaternion targetRotation = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
		}

	}

	void GetNextPathNode()
	{
		if (pathNodeIndex < pathGO.transform.childCount)
		{
			targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
			pathNodeIndex++;
		}
		else
		{
			targetPathNode = null;
			ReachGoal();
		}
	}

	void ReachGoal()
	{
		GameObject.FindObjectOfType<ScoreManager>().LoseLife();
		Die();
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Die();
		}

		HealthBar.fillAmount = health / maxHealth;
	}

	private void Die()
	{
		// TODO: DO this more safely!
		Destroy(Instantiate(DeathEffectParticlePrefab, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), DeathEffectParticlePrefab.transform.rotation), 3);
		GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
		GameObject.FindObjectOfType<EnemyManager>().AliveEnemies.Remove(enemyId);
		Debug.Log(GameObject.FindObjectOfType<EnemyManager>().AliveEnemies.Count);
		Destroy(gameObject);
	}
}
