using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;



public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	[SerializeField]
	private PlayerObject _playerObject;
	public PlayerObject PlayerObject { get {return _playerObject; } set { _playerObject = value; } }

	public Dictionary<string, EnemyObject> crntEnemyDictionary = new Dictionary<string, EnemyObject> ();

	public GameObject swordReachMarker;

	private static Dictionary<string, Sprite[]> loadedSpriteDict = new Dictionary<string, Sprite[]>();

	public GameObject CurrentStageObject { get; set; }


	void Start() {
		//データ初期化
		SwordDataTableObject.Instance.InitData ();
		GunDataTableObject.Instance.InitData ();
		CraftItemDataTableObject.Instance.InitData ();
		StageDataTableObject.Instance.InitData ();

		Scene scene = SceneManager.GetActiveScene();
		//プレイヤー初期化
		if (scene.name == "Main") {
			PlayerData.InitPlayerData ();
		}

		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		_playerObject = playerObj.GetComponent<PlayerObject> ();
		_playerObject.transform.localPosition = new Vector3 (-4.0f, 0.0f, 0.0f);

		_playerObject.InitChar (new CharaData());
		_playerObject.InitCharGunObject ("gun2");
		_playerObject.InitCharSwordObject ("swd2");

		//NPC初期化
		InitNpcObject ();

		//ステージ初期化
		if (scene.name == "Main") {
			Debug.LogError ("********************: " + PlayerData.crntStageID);
			InitStage (PlayerData.crntStageID);
		}
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
			NpcObject npcObject = InitCharObject<NpcObject> (charData);

			if (npcObject.charaData.BattlePosition == 1) {
				npcObject.transform.position = new Vector3 (-4.8f, 0.0f, -0.2f);
			}
		}
	}

	public T InitCharObject<T>(CharaData charData) where T: AbstractCharacterObject {
		GameObject charObject = (GameObject)Instantiate(Resources.Load("Prefabs/Characters/" + charData.BodyPrefab));
		charObject.AddComponent<T> ();

		charObject.GetComponent<T> ().InitChar(charData);
		charObject.GetComponent<T> ().InitCharGunObject (charData.GunID);
		charObject.GetComponent<T> ().InitCharSwordObject (charData.SwordID);

		return charObject.GetComponent<T>();
	}


	private void InitStage(string stageId) {
		StageData crntStageData = StageDataTableObject.Instance.Table.All.First (stgData => stgData.ID == stageId); //一番最初のだからWhereではなくFirstを使う。Whereは複数
		if (CurrentStageObject != null) {
			DestroyImmediate (CurrentStageObject);
		}
		CurrentStageObject = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + crntStageData.BG));
		CurrentStageObject.GetComponent<StageObject> ().InitStageObject (crntStageData);
	}
		

	public static GameObject getChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}

	public void InitResult() {
		UiController.Instance.OpenDialogPanel("Result", ()=>{
			int stgNum = Int32.Parse(PlayerData.crntStageID.Replace("stg", ""));
			stgNum++;
			string nextStgId = "stg"+stgNum;
			StageData nextStage = StageDataTableObject.Instance.Table.All.FirstOrDefault(stgData => stgData.ID == nextStgId);
			if(nextStage != null) {
				PlayerData.crntStageID = nextStgId;
				TransitionManager.Instance.FadeTo ("Main");
			}
			//なかった場合はMapを開く
			else {
				UiController.Instance.OpenMapPanel();
			}
		});
	}
}
