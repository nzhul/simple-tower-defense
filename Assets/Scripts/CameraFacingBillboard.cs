using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
	Camera main;

	void Start()
	{
		main = Camera.main;
	}

	void Update()
	{
		transform.LookAt(transform.position + main.transform.rotation * Vector3.forward,
			main.transform.rotation * Vector3.up);
	}
}