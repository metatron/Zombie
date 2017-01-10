using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour {
	[SerializeField]
	private float velocity = 2000.0f;
	public float Velocity { get { return velocity; } set { velocity = value; } }

	[SerializeField]
	private float damage = 1.0f;
	public float Damage { get { return damage; } set { damage = value; } }

	void Start() {
		Destroy (gameObject, 3.0f);
	}

	void OnCollisionEnter(Collision other) {
		EnemyObject enemyObj = other.gameObject.GetComponent<EnemyObject> ();
		if (enemyObj != null) {
			Destroy (gameObject);
			Destroy (other.gameObject);
		}
	}
}
