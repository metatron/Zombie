using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractDamageObject : MonoBehaviour {
	[SerializeField]
	private AbstractWeaponObject _weapon;
	public AbstractWeaponObject WeaponObject { get { return _weapon; } set { _weapon = value; } }

	public virtual void CalculateDamage(EnemyObject enemyObject) {
		Debug.LogError ("_weapon: " + _weapon + ", dmg: " + _weapon.Damage);
		enemyObject.Hp -= (int)System.Math.Ceiling (_weapon.Damage);
		if (enemyObject.Hp <= 0) {
			Destroy (enemyObject.gameObject);
		}
	}

//	protected virtual void OnCollisionEnter(Collision other) {
	void OnTriggerEnter(Collider other) {
		EnemyObject enemyObj = other.gameObject.GetComponent<EnemyObject> ();
		if (enemyObj != null) {
			CalculateDamage (enemyObj);
		}
		//Destroy (gameObject);
		gameObject.GetComponent<Collider> ().enabled = false;

		//弾は当たった瞬間消す。
		if(GetType() == typeof(BulletObject)) {
			Destroy(gameObject);
		}
		//剣の場合は数秒後に消す
		else {
			StartCoroutine (DestroyEffect());
		}
	}

	private IEnumerator DestroyEffect() {
		yield return new WaitForSeconds (3.0f);
		Destroy (gameObject);
	}
}
