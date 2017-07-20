﻿using System;
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

	//ゲームのポーズフラグのON/OFF
	private bool _pauseGame = false;
	public bool PauseGame { get {return _pauseGame; } set { _pauseGame = value; } }

	//ステージセレクト時、ハンガーレベルが0のもの（ステージを始めたら死亡
	private List<CharaData> _deadMenList = new List<CharaData>();
	public List<CharaData> DeadMenList { get { return _deadMenList; } set { _deadMenList = value; } }

	void Start() {
		//データ初期化
		SwordDataTableObject.Instance.InitData ();
		GunDataTableObject.Instance.InitData ();
		CraftItemDataTableObject.Instance.InitData ();
		StageDataTableObject.Instance.InitData ();
		RarityDataTableObject.Instance.InitData ();
		ExperienceDataTableObject.Instance.InitData ();
		ClothDataTableObject.Instance.InitData ();
		ToolItemDataTableObject.Instance.InitData ();

		Scene scene = SceneManager.GetActiveScene();
		//プレイヤー初期化
		if (PlayerData.playerCharData == null) {
			PlayerData.InitPlayerData ();
		}

		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		if (playerObj != null) {
			_playerObject = playerObj.GetComponent<PlayerObject> ();
			_playerObject.transform.localPosition = new Vector3 (-4.0f, -0.2f, 0.0f);

			_playerObject.InitChar (PlayerData.playerCharData);
			_playerObject.InitCharGunObject (PlayerData.playerCharData.GunID);
			_playerObject.InitCharSwordObject (PlayerData.playerCharData.SwordID);
		}

		//NPC初期化
		InitNpcObject ();

		//ステージ初期化
		if (scene.name == "Main") {
			Debug.LogError ("********************: " + PlayerData.crntStageID);
			InitStage (PlayerData.crntStageID);
		}
		//キャラチェック
		else if (scene.name == "CharacterTest") {
			GameObject femaleObj = GameObject.Find ("Female1");
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.HAIR, "fc1");
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.EYES, "fc1", Color.red);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.ARM_L, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.ARM_R, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.BODY, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.CHEST, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.HIP, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.LEG_UPPER_L, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.LEG_UPPER_R, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.LEG_LOWER_L, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.LEG_LOWER_R, "fc1", Color.black);
//			femaleObj.GetComponent<ClothingSystem> ().SetClothParts (ClothingSystem.ClothParts.SHOES, "fc1", Color.white);

			string dataStr = ClothingSystem.AutoClothGenerator (CharaData.Gender.Female);//, femaleObj.GetComponent<ClothingSystem> ());
//			string dataStr = "HAIR@fc1@0.719169&0.447556&0.4697548|EYES@fc1@0.7424355&0.2206098&0.7012246|CHEST@fc1@0.03907001&0.8151758&0.9653349|BODY@fc1@0.6807265&0.6312014&0.2544544|ARM_L@fc1@0.269041&0.2703423&0.1016339|ARM_R@fc1@0.269041&0.2703423&0.1016339|HIP@fc1@0.7930905&0.9369744&0.877869|LEG_UPPER_L@fc1@0.8081599&0.3799738&0.527169|LEG_UPPER_R@fc1@0.8081599&0.3799738&0.527169|LEG_LOWER_L@fc1@0.8081599&0.3799738&0.527169|LEG_LOWER_R@fc1@0.8081599&0.3799738&0.527169";
			femaleObj.GetComponent<ClothingSystem> ().SetClothPartsByStringData (dataStr);
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

	public Sprite GetSpriteFromID(string id) {
		//Search Sword Items
		AbstractData searchingData = SwordDataTableObject.Instance.GetParams (id);
		if (searchingData != null) {
			return GetSpriteFromPath("WeaponAtlas", searchingData.Image);
		}

		//Search Gun Items
		searchingData = GunDataTableObject.Instance.GetParams (id);
		if (searchingData != null) {
			return GetSpriteFromPath("WeaponAtlas", searchingData.Image);
		}

		//Search Tool Items
		searchingData = ToolItemDataTableObject.Instance.GetParams (id);
		if (searchingData != null) {
			return GetSpriteFromPath("ItemAtlas", searchingData.Image);
		}

		//Search Material Items
		searchingData = CraftItemDataTableObject.Instance.GetParams (id);
		if (searchingData != null) {
			return GetSpriteFromPath("ItemAtlas", searchingData.Image);
		}

		return null;
	}


	public void InitNpcObject() {
		foreach (CharaData charData  in PlayerData.playerNpcDictionary.Values) {
			if (charData.BattlePosition == -1 || charData.IsDead) {
				continue;
			}

			NpcObject npcObject = InitCharObject<NpcObject> (charData);
			//プレイヤーの隣
			if (npcObject.charaData.BattlePosition == 0) {
				npcObject.transform.position = new Vector3 (-4.7f, -0.4f, -0.2f);
			} 
			//その他のWallのポジションの箇所にセット（バトル画面のみ）
			else {
				Scene scene = SceneManager.GetActiveScene();
				if (scene.name == "Main") {
					Transform wallObj = WallController.Instance.GetWallObject (charData.BattlePosition).transform;
					npcObject.transform.SetParent (wallObj);
					npcObject.transform.localPosition = new Vector3(0.0f, -0.3f, 0.0f);
				}
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


	public void InitStage(string stageId) {
		StageData crntStageData = StageDataTableObject.Instance.Table.All.First (stgData => stgData.ID == stageId); //一番最初のだからWhereではなくFirstを使う。Whereは複数
		if (CurrentStageObject != null) {
			DestroyImmediate (CurrentStageObject);
		}
		//敵があまっていれば削除
		DeleteAllEnemies();

		CurrentStageObject = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/" + crntStageData.BG));
		CurrentStageObject.GetComponent<StageObject> ().InitStageObject (crntStageData);

		//バトル参加してるキャラのハンガーレベル調整
		ChangeHungerLevel();
	}

	public void DeleteAllEnemies() {
		if(crntEnemyDictionary.Count > 0) {
			foreach (string enemyId in crntEnemyDictionary.Keys) {
				if (crntEnemyDictionary [enemyId] != null) {
					Destroy (crntEnemyDictionary [enemyId].gameObject);
				}
			}
			crntEnemyDictionary.Clear();
		}
	}
		

	public void InitResult() {
		//ドロップのトータルを表示
		string dropResult = "";
		List<DropData> dropList = CurrentStageObject.GetComponent<StageObject> ().DropDataList;
		foreach (DropData drop in dropList) {
			if (CurrentStageObject.GetComponent<StageObject> ().DropItemNum.ContainsKey (drop.ID)) {
				dropResult += drop.ID + ": " + CurrentStageObject.GetComponent<StageObject> ().DropItemNum [drop.ID] + "\n";
			}
		}

		//Npcドロップを表示＆追加
		string npcResult = "";
		CharaData dropNpcData = CurrentStageObject.GetComponent<StageObject> ().DropNpcData;
		if (dropNpcData != null) {
			PlayerData.AddNpcData (dropNpcData);
			npcResult = "NPC added! \n MAX ATK: " + dropNpcData.MaxAtk;
		}


		//ハンガーレベルが0になってるやつを死亡させる。
		List<CharaData> killedCharList = KillHungeryCharas();

		//ステージクリア情報表示
		UiController.Instance.OpenDialogPanel("Result\n\n" + dropResult + "\n" + npcResult, ()=>{
			//死亡キャラがいれば表示

			//OKボタンでステージ移動。
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


	/**
	 * 
	 * ハンガーレベル調整
	 * バトル参加: -20pt
	 * その他: -10pt
	 * 
	 */
	private void ChangeHungerLevel() {
		//プレイヤーは必ず参加なので-20.
		PlayerData.playerCharData.hunger -= 20.0f;
		//最低0.0f
		PlayerData.playerCharData.hunger = Mathf.Max (0.0f, PlayerData.playerCharData.hunger);

		//NPC
		foreach (CharaData charData in PlayerData.playerNpcDictionary.Values) {
			//バトル参加
			if (charData.BattlePosition > 0) {
				charData.hunger -= 20.0f;
			}
			//その他
			else {
				charData.hunger -= 10.0f;
			}

			//最低0.0f
			charData.hunger = Mathf.Max (0.0f, charData.hunger);
		}
	}

	/**
	 * 
	 * 戦闘開始時にチェック。
	 * hungerWarningListに値が存在すればワーニングを出す。
	 * 
	 * 
	 */
	public List<CharaData> CheckHungerLevel() {
		List<CharaData> hungerWarningList = new List<CharaData> ();

		//player check.
		if (PlayerData.playerCharData.hunger <= 0.0f) {
			hungerWarningList.Add (PlayerData.playerCharData);
		}

		//npc check
		foreach (CharaData charData in PlayerData.playerNpcDictionary.Values) {
			if (charData.hunger <= 0.0f) {
				hungerWarningList.Add (charData);
			}
		}

		return hungerWarningList;
	}

	/**
	 *
	 * ステージクリア時にハンガーレベルが0の奴らを殺す。
	 * 死んだキャラクターのリストを返す。
	 * 
	 */
	public List<CharaData> KillHungeryCharas() {
		List<CharaData> killedCharaList = new List<CharaData> ();

		//player check.
		if (PlayerData.playerCharData.hunger <= 0.0f) {
			PlayerData.playerCharData.IsDead = true;
			killedCharaList.Add (PlayerData.playerCharData);
		}

		//npc check
		foreach (CharaData charData in PlayerData.playerNpcDictionary.Values) {
			if (charData.hunger <= 0.0f) {
				charData.IsDead = true;
				killedCharaList.Add (charData);
			}
		}

		return killedCharaList;
	}


}
