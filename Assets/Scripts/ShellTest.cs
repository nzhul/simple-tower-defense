using UnityEngine;
using System.Collections;

public class ShellTest : MonoBehaviour {

	public Transform shellTarget;
	public GameObject shellPrefab;

	void Update () {
		if (Input.GetKeyDown(KeyCode.T))
		{
			GameObject go = Instantiate(shellPrefab, transform.position, transform.rotation) as GameObject;
			Shell goShell = go.GetComponent<Shell>();
			goShell.target = shellTarget;

		}
	}
}
