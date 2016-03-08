using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	public float distance = 60f;
	public float sensitivityDistance = 50;
	public float damping = 5;
	public float minFOV = 40;
	public float maxFOV = 60;
 
	void Start()
	{
		distance = GetComponent<Camera>().fieldOfView;
	}

	void Update()
	{

		distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityDistance;
		distance = Mathf.Clamp(distance, minFOV, maxFOV);
		GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, distance, Time.deltaTime * damping);
	}
}