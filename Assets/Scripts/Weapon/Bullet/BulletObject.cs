using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : AbstractDamageObject {
	[SerializeField]
	private float velocity = 2000.0f;
	public float Velocity { get { return velocity; } set { velocity = value; } }

	void Start() {
		Destroy (gameObject, 3.0f);
	}

}
