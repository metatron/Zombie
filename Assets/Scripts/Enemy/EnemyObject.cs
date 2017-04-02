using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : AbstractCharacterObject {
	[SerializeField]
	public EnemyData EnemyData { get; set; }

	public bool IsBoss = false;

	//SpawnerでInstantiateされる毎に設定。
	public DropData dropData;

	void Update() {
		if (GameManager.Instance.PauseGame) {
			return;
		}

		transform.Translate (Vector3.right * EnemyData.Speed * -Time.deltaTime);
	}
}
