﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class TowerSpot : MonoBehaviour, IPointerClickHandler
{
	public GameObject towerSelectionUI;
	public Transform towerSpots;

	void Start()
	{
		towerSpots = GameObject.Find("TowerSpots").transform;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (towerSelectionUI != null)
		{
			ClearAllOtherUIElements();
			towerSelectionUI.SetActive(true);
		}
	}

	private void ClearAllOtherUIElements()
	{
		foreach (Transform towerSpot in towerSpots)
		{
			GameObject currentTowerUICanvas = towerSpot.FindChild("SelectTowerCanvas").gameObject;
			currentTowerUICanvas.SetActive(false);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (towerSelectionUI != null)
			{
				towerSelectionUI.SetActive(false);
			}
		}
	}
}
