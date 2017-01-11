using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunObject : AbstractWeaponObject {
	[SerializeField]
	private BulletObject _bulletObject;
	public BulletObject BulletObject { get { return _bulletObject; } set { _bulletObject = value; } }

	[SerializeField]
	private int maxAmmo;

	[SerializeField]
	private float rapidFireTime;	//1つ1つの弾を打つスピード

	[SerializeField]
	private float reloadTime;		//全弾打った後のリロードタイム

	public void Fire() {
		GameObject instBullet = (GameObject)Instantiate (_bulletObject.gameObject);
		instBullet.GetComponent<BulletObject> ().GunObject = this;
		instBullet.transform.localPosition = transform.localPosition;
		instBullet.GetComponent<Rigidbody> ().AddForce (new Vector3 (instBullet.GetComponent<BulletObject>().Velocity, 0.0f, 0.0f));
	}

}
