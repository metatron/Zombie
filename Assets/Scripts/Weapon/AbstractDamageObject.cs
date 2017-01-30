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

	protected virtual void OnCollisionEnter(Collision other) {
		EnemyObject enemyObj = other.gameObject.GetComponent<EnemyObject> ();
		if (enemyObj != null) {
			CalculateDamage (enemyObj);
		}
		Destroy (gameObject);
	}
}
