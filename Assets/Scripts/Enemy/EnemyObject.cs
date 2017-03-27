using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : AbstractCharacterObject {
	[SerializeField]
	private int _hp = 3;
	public int Hp { get { return _hp; } set {_hp = value; } }

	[SerializeField]
	private float _speed = 1.0f;
	public float Speed { get { return _speed; } set {_speed = value; } }

	public bool IsBoss = false;

	//SpawnerでInstantiateされる毎に設定。
	public DropData dropData;

	void Update() {
		transform.Translate (Vector3.right * Speed * -Time.deltaTime);
	}
}
