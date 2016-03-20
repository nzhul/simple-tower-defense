using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class TowerSelectionIcon : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
	public GameObject towerPrefab;
	public GameObject towerSpot; 

	public void OnPointerClick(PointerEventData eventData)
	{
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if (sm.Money < towerPrefab.GetComponent<BaseTower>().cost)
		{
			Debug.Log("Not enough money!");
			return;
		}
		else
		{
			sm.Money -= towerPrefab.GetComponent<BaseTower>().cost;
		}

		Instantiate(towerPrefab, towerSpot.transform.position, towerSpot.transform.rotation);
		Destroy(towerSpot.gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Item 1 enter");
	}
}
