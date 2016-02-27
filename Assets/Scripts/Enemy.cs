using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{

	GameObject pathGO;
	Transform targetPathNode;
	int pathNodeIndex = 0;

	public float speed = 10;
	public float health = 1f;
	public int moneyValue = 10;

	void Start()
	{
		pathGO = GameObject.Find("Path");
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
		Destroy(gameObject);
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// TODO: DO this more safely!
		GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
		Destroy(gameObject);
	}
}
