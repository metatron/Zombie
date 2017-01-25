using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	[SerializeField]
	private PlayerObject _playerObject;
	public PlayerObject PlayerObject { get {return _playerObject; } set { _playerObject = value; } }

	public Dictionary<string, EnemyObject> crntEnemyDictionary = new Dictionary<string, EnemyObject> ();

	void Start() {
		_playerObject.InitPlayer ();
		_playerObject.InitPlayerGunObject ("Prefabs/Gun01");
		_playerObject.InitPlayerSwordObject ("Prefabs/Sword01");
	}


	/**
	 * 
	 * targetと現存する敵の距離
	 * の中で一番近いものを<距離、EnemyObjectのKeyValueで返す。
	 * 存在しない場合は EnemyObjectはNULL
	 * 
	 */
	public KeyValuePair<float, EnemyObject> GetClosestEnemyObjectTo(GameObject target) {
		float minDist = float.MaxValue;
		float nearestEnemyObj = null;
		foreach (KeyValuePair<string, EnemyObject> pair in GameManager.Instance.crntEnemyDictionary) {
			if (pair.Value == null) {
				continue;
			}

			float comparingDist = Vector3.Distance (target.transform.position, pair.Value.transform.position);

			if (comparingDist < minDist) {
				minDist = comparingDist;
				nearestEnemyObj = pair.Value;
			}
		}

		return new KeyValuePair<float, EnemyObject>(minDist, nearestEnemyObj);
	}
}
