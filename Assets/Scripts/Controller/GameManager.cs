﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;


public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	[SerializeField]
	private PlayerObject _playerObject;
	public PlayerObject PlayerObject { get {return _playerObject; } set { _playerObject = value; } }

	public Dictionary<string, EnemyObject> crntEnemyDictionary = new Dictionary<string, EnemyObject> ();

	public GameObject swordReachMarker;

	private static Dictionary<string, Sprite[]> loadedSpriteDict = new Dictionary<string, Sprite[]>();

	void Start() {
		SwordDataTableObject.Instance.InitData ();

		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		_playerObject = playerObj.GetComponent<PlayerObject> ();
		_playerObject.transform.localPosition = new Vector3 (-4.0f, 0.0f, 0.0f);

		_playerObject.InitChar (0);
		_playerObject.InitCharGunObject ("Prefabs/Gun01");
		_playerObject.InitCharSwordObject ("swd2");

		PlayerData.InitPlayerData ();

		InitNpcObject ();
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


	public Sprite GetSpriteFromPath(string atlasName, string spriteName) {
		Sprite[] loadedSprites;
		if (!loadedSpriteDict.ContainsKey (atlasName)) {
			loadedSprites = Resources.LoadAll<Sprite> ("Atlases/" + atlasName);
			loadedSpriteDict.Add (atlasName, loadedSprites);
		} 
		//load from Dictionary
		else {
			loadedSprites = loadedSpriteDict [atlasName];
		}
		Sprite sp = System.Array.Find<Sprite> (loadedSprites, (sprite) => sprite.name.Equals (spriteName));
		return sp;
	}


	public void InitNpcObject() {
		foreach (CharaData charData  in PlayerData.playerNpcDictionary.Values) {
			GameObject npcObject = (GameObject)Instantiate(Resources.Load(charData.BodyPrefab));
			npcObject.AddComponent<NpcObject> ();
			npcObject.GetComponent<NpcObject> ().InitChar(0);
			npcObject.GetComponent<NpcObject> ().SetSortingLayer ("Forward1");
		}
	}




	public static GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
}
