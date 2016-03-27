using UnityEngine;
using System.Collections;

public class Turret : BaseTower {

	public Transform turretTransform;
	public Transform muzzle1;
	public Transform muzzle2;
	public AudioClip shootAudio;

	protected override void OnUpdate(params object[] values)
	{
		Quaternion lookRot = Quaternion.LookRotation((Vector3)values[0]);
		turretTransform.rotation = Quaternion.Euler(270, lookRot.eulerAngles.y - 180, 0);
	}

	protected override void OnShoot()
	{
		muzzle1.transform.Rotate(0, 0, 400 * Time.deltaTime);
		muzzle2.transform.Rotate(0, 0, 400 * Time.deltaTime);
		base.OnShoot();

		// TODO: Spawn bullet shells ( гилзи )
	}

	protected override void ShootAt(GameObject e)
	{
		GameObject bullet1 = (GameObject)Instantiate(base.projectilePrefab, muzzle1.transform.position, transform.rotation);
		Bullet b1 = bullet1.GetComponent<Bullet>();
		b1.target = e.transform;
		b1.damage = damage;
		b1.radius = radius;

		GameObject bullet2 = (GameObject)Instantiate(base.projectilePrefab, muzzle2.transform.position, transform.rotation);
		Bullet b2 = bullet2.GetComponent<Bullet>();
		b2.target = e.transform;
		b2.damage = damage;
		b2.radius = radius;

		float customVolumePercent = .2f;
		AudioManager.instance.PlaySound(shootAudio, transform.position, customVolumePercent);
	}
}
