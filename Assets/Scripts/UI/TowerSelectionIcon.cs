using Assets.Scripts.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelectionIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public GameObject towerPrefab;
	public TowerInfo towerPrefabInfo;
	public GameObject towerSpot;
	public Tooltip tooltip;

	void Start()
	{
		towerPrefabInfo = GetTowerInfo(towerPrefab);
    }

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

		tooltip.Deactivate();
		Instantiate(towerPrefab, towerSpot.transform.position, towerSpot.transform.rotation);
		Destroy(towerSpot.gameObject);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("Item 1 enter");
		// TODO: Add another hover UI element that gives details about the tower and its cost
		tooltip.Activate(towerPrefabInfo);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.Deactivate();
	}

	private TowerInfo GetTowerInfo(GameObject towerPrefab)
	{
		if (towerPrefab != null)
		{
			BaseTower tower = towerPrefab.GetComponent<BaseTower>();
			TowerInfo newInfo = new TowerInfo();
			newInfo.Cost = tower.cost;
			newInfo.Damage = tower.damage;
			newInfo.Range = tower.range;
			newInfo.ReloatTime = tower.fireCooldown;
			newInfo.Name = tower.name;
			newInfo.Description = tower.description;

			return newInfo;
		}

		return null;
	}
}
