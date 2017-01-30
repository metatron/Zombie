using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractDamageObject : MonoBehaviour {
	
	
	public virtual void CalculateDamage(EnemyObject enemyObject) {
		enemyObject.Hp -= Mathf.CeilToInt(_damage);
		if (enemyObject.Hp <= 0) {
			Destroy (enemyObject.gameObject);
		}

	}
}
