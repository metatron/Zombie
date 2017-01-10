using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour {
	public const float SELF_DESTROY_DIST = 300.0f;

	[SerializeField]
	private float velocity = 2000.0f;
	public float Velocity { get { return velocity; } set { velocity = value; } }

	[SerializeField]
	private float damage = 1.0f;
	public float Damage { get { return damage; } set { damage = value; } }

	void Update() {
		if (transform.localPosition.x > SELF_DESTROY_DIST) {
			Destroy (gameObject);
		}
	}
}
