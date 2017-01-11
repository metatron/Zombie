using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	[SerializeField]
	private PlayerObject _playerObject;
	public PlayerObject PlayerObject { get {return _playerObject; } set { _playerObject = value; } }

	void Start() {
		_playerObject.initPlayerGunObject ("Prefabs/Gun01");
	}
}
