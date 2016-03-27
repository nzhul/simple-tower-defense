using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.UI;

public class Tooltip : MonoBehaviour
{
	private GameObject tooltip;

	void Start()
	{
		tooltip = GameObject.Find("Tooltip");
		tooltip.SetActive(false);
	}

	void Update()
	{
		if (gameObject.activeSelf)
		{
			gameObject.transform.position = Input.mousePosition;
		}
	}

	public void Activate(TowerInfo towerInfo)
	{
		ConstructDataString(towerInfo);
		gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

	public void ConstructDataString(TowerInfo towerInfo)
	{
		string data = "<size=20><color=#000000><b>" + towerInfo.Name + "</b></color></size>\n\n"
			+ towerInfo.Description
			+ "\n\nDamage: " + towerInfo.Damage
			+ "\nReloadTime: " + towerInfo.ReloatTime + " seconds"
			+ "\nRange: " + towerInfo.Range
			+ "\n\n<size=20><color=#000000>Cost: " + towerInfo.Cost + "</color></size>";
		//string data = towerInfo.Cost.ToString();
		tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
	}

}