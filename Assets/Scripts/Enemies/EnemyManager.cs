using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public int currentEnemyId = 0;
	public Dictionary<int, GameObject> AliveEnemies;

	// Use this for initialization
	void Start () {
		AliveEnemies = new Dictionary<int, GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
