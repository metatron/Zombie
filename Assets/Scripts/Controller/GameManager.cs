using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	[SerializeField]
	private PlayerObject _playerObject;
	public PlayerObject PlayerObject { get {return _playerObject; } set { _playerObject = value; } }

	public Dictionary<string, EnemyObject> crntEnemyDictionary = new Dictionary<string, EnemyObject> ();

	public GameObject swordReachMarker;

	void Start() {
		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		_playerObject = playerObj.GetComponent<PlayerObject> ();
		_playerObject.transform.localPosition = new Vector3 (-4.0f, 0.0f, 0.0f);

		_playerObject.InitPlayer ();
		_playerObject.InitPlayerGunObject ("Prefabs/Gun01");
		_playerObject.InitPlayerSwordObject ("Prefabs/Sword01");

	}

	/**
	 * 
	 * targetと現存する敵の距離
	 * の中で一番近いものを<距離、EnemyObjectを返す。
	 * 存在しない場合は EnemyObjectはNULL
	 * 
	 */
	public EnemyObject GetClosestEnemyObjectTo(GameObject target, ref float distance) {
		float minDist = float.MaxValue;
		EnemyObject nearestEnemyObj = null;
		List<EnemyObject> enemyList = GameManager.Instance.crntEnemyDictionary.Values.ToList();
		for(int i=0; i<enemyList.Count; i++) {
			if (enemyList [i] == null) {
				continue;
			}

			float comparingDist = Vector3.Distance (target.transform.position, enemyList[i].transform.position);

			if (comparingDist < minDist) {
				minDist = comparingDist;
				nearestEnemyObj = enemyList [i];
			}
		}

		distance = minDist;
		return nearestEnemyObj;
	}
}
