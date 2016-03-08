using UnityEngine;

public class CameraPan : MonoBehaviour {

	public float CamSpeed = .2f;
	public int GUISize = 500;

	void Update()
	{
		var recdown = new Rect(0, 0, Screen.width, GUISize);

		var recup = new Rect(0, Screen.height - GUISize, Screen.width, GUISize);

		var recleft = new Rect(0, 0, GUISize, Screen.height);

		var recright = new Rect(Screen.width - GUISize, 0, GUISize, Screen.height);

		if (Input.GetMouseButton(1))
		{
			if (recdown.Contains(Input.mousePosition))
			{
				transform.Translate(0, 0, -CamSpeed, Space.World);
			}

			if (recup.Contains(Input.mousePosition))
			{
				transform.Translate(0, 0, CamSpeed, Space.World);
			}

			if (recleft.Contains(Input.mousePosition))
			{
				transform.Translate(-CamSpeed, 0, 0, Space.World);
			}

			if (recright.Contains(Input.mousePosition))
			{
				transform.Translate(CamSpeed, 0, 0, Space.World);
			}
		}
	}
}
