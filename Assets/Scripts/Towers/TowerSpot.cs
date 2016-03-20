using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class TowerSpot : MonoBehaviour, IPointerClickHandler
{
	public GameObject towerSelectionUI;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (towerSelectionUI != null)
		{
			towerSelectionUI.SetActive(true);
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
