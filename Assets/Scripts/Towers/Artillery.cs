using UnityEngine;
using System.Collections;

public class Artillery : BaseTower {

	public Transform silo;
	private bool isReloading;
	public float reloadTime = 1f;

	protected override void ShootAt(GameObject e)
	{
		GameObject bullet1 = (GameObject)Instantiate(base.projectilePrefab, transform.position, transform.rotation);
		Shell b1 = bullet1.GetComponent<Shell>();
		b1.damage = base.damage;
		b1.radius = base.radius;
		b1.target = e.transform;
	}

	protected override void OnShoot()
	{
		if (silo != null)
		{
			//StartCoroutine(AnimateSilo());
		}

		// TODO: Move the silo down fast and then slow up
		// Spawn fire/smoke effect
	}

	IEnumerator AnimateSilo()
	{
		isReloading = true;
		yield return new WaitForSeconds(.2f);

		float reloadSpeed = 1f / reloadTime;
		float percent = 0;
		Vector3 initialPosition = silo.position;
		float maxRaiseDistance = .2f;

		while (percent < 1)
		{
			percent += Time.deltaTime * reloadSpeed;
			float reloadHeight = Mathf.Lerp(0, maxRaiseDistance, percent);
			silo.position = new Vector3(silo.position.x, silo.position.y + reloadHeight, silo.position.z);

			yield return null;
		}

		isReloading = false;
	}
}
