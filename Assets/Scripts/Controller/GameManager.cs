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
}
