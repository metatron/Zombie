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
		GameManager.Instance.PlayerObject.Play ("fire");
		GameObject instBullet = (GameObject)Instantiate (_bulletObject.gameObject);
		instBullet.GetComponent<AbstractDamageObject> ().WeaponObject = this;

		instBullet.transform.SetParent (transform);
		instBullet.transform.localPosition = Vector3.zero;
		instBullet.GetComponent<Rigidbody> ().AddForce (new Vector3 (instBullet.GetComponent<BulletObject>().Velocity, 0.0f, 0.0f));
	}

}
