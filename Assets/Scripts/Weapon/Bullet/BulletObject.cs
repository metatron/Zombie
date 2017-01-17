using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour {
	[SerializeField]
	private float velocity = 2000.0f;
	public float Velocity { get { return velocity; } set { velocity = value; } }

	[SerializeField]
	private GunObject _gunObject;
	public GunObject GunObject { get { return _gunObject; } set { _gunObject = value; } }

	[SerializeField]
	private GunObject _swordObject;
	public GunObject SwordObject { get { return _swordObject; } set { _swordObject = value; } }

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
