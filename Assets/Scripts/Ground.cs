using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Ground : MonoBehaviour, IPointerClickHandler
{
	public Transform towerSpots;

	void Start()
	{
		towerSpots = GameObject.Find("TowerSpots").transform;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		foreach (Transform towerSpot in towerSpots)
		{
			GameObject currentTowerUICanvas = towerSpot.FindChild("SelectTowerCanvas").gameObject;
			currentTowerUICanvas.SetActive(false);
		}
	}
}
