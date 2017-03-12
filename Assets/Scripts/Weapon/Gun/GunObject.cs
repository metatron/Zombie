using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunObject : AbstractWeaponObject {
	[SerializeField]
	private BulletObject _bulletObject;
	public BulletObject BulletObject { get { return _bulletObject; } set { _bulletObject = value; } }

	[SerializeField]
	private int _maxAmmo;
	private int _crntAmmo;

	[SerializeField]
	private float _reloadTime;		//全弾打った後のリロードタイム
	private bool isReloading = false;

	public void CopyParamsFrom(string id) {
		GunData gunData = GunDataTableObject.Instance.GetParams (id);
		Name = gunData.Name;
		Damage = gunData.Damage;
		_maxAmmo = gunData.MaxAmmo;
		_crntAmmo = _maxAmmo;
		_reloadTime = gunData.ReloadTime;
		GetComponent<SpriteRenderer> ().sprite = LoadSprite (gunData.Image);

		_bulletObject = Resources.Load<GameObject> ("Prefabs/Items/Bullets/" + gunData.BulletPrefab).GetComponent<BulletObject>();
	}

	public void Fire() {
		if (isReloading) {
			return;
		}

		Owner.GetComponent<AbstractCharacterObject>().Play ("fire");
		GameObject instBullet = (GameObject)Instantiate (_bulletObject.gameObject);
		instBullet.GetComponent<AbstractDamageObject> ().WeaponObject = this;

		instBullet.transform.SetParent (transform);
		instBullet.transform.localPosition = Vector3.zero;
		instBullet.GetComponent<Rigidbody> ().AddForce (new Vector3 (instBullet.GetComponent<BulletObject>().Velocity, 0.0f, 0.0f));
		//発射位置を整えた後は親をグローバルに戻す。
		instBullet.transform.SetParent (null);

		_crntAmmo--;
		if (_crntAmmo == 0) {
			StartCoroutine (Reloading ());
		}
	}

	private IEnumerator Reloading() {
		Debug.LogError ("Reloading...");

		isReloading = true;
		yield return new WaitForSeconds (_reloadTime);

		Debug.LogError ("Reloaded!");

		//reload completed
		isReloading = false;
		_crntAmmo = _maxAmmo;
	}

}
