using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : AbstractCharacterObject {
	[SerializeField]
	private int _hp = 3;
	public int Hp { get { return _hp; } set {_hp = value; } }

	[SerializeField]
	private float _speed = 100.0f;
	public float Speed { get { return _speed; } set {_speed = value; } }

	void Start () {
		GetComponent<Rigidbody> ().AddForce (new Vector3 (-_speed, 0.0f, 0.0f), ForceMode.Acceleration);
	}
}
