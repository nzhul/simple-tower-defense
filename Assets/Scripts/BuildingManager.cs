using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public GameObject selectedTower;

	public void SelectTowerType(GameObject prefab)
	{
		selectedTower = prefab;
	}
}
