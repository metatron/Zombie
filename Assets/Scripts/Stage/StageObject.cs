using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour {
	private StageData _stageData;
	public StageData StageData { get; }

	public List<Spawner> spawnerList = new List<Spawner>();

	private float cumulativeTime = 0.0f; //StageObjectが


	private List<DropData> dropDataList = new List<DropData> ();
	public List<DropData> DropDataList { get { return dropDataList; } }

	//クリア後、ドロップの有無を確認する為に必要。追加はAbstractDamageObjectで行う。
	public Dictionary<string, int> DropItemNum = new Dictionary<string, int>();

	public void InitStageObject(StageData stageData) {
		_stageData = stageData;
		//ドロップアイテムを初期化
		ParseDropData (stageData.Drops);

		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;

		//Spawner1
		if(!string.IsNullOrEmpty(stageData.Spawn1)) {
			EnemyData enemy = ParseEnemyData (stageData.Spawn1);
			spawnerList[0].spawn = true;
			spawnerList[0].StartSpawning (enemy);
		}

		//Spawner2
		if(!string.IsNullOrEmpty(stageData.Spawn2)) {
			EnemyData enemy = ParseEnemyData (stageData.Spawn2);
			spawnerList[1].spawn = true;
			spawnerList[1].StartSpawning (enemy);
		}

		//Spawner3
		if(!string.IsNullOrEmpty(stageData.Spawn2)) {
			EnemyData enemy = ParseEnemyData (stageData.Spawn2);
			spawnerList[2].spawn = true;
			spawnerList[2].StartSpawning (enemy);
		}

	}

	void FixedUpdate() {
		if (GameManager.Instance.PauseGame) {
			return ;
		}

		//Spawnerが終ってるかどうか定期的に確認
		foreach (Spawner spawner in spawnerList) {
			//TimeTillBossが設定されてなくて、ザコ敵の排出が終ってたらbossをInit
			if (_stageData.TimeTillBoss <= 0 && spawner.IsFinishedSpawning () && !spawner.IsBossInited) {
				StartCoroutine (InitBoss (spawner));
				spawner.IsBossInited = true;
			}
			//TimeTillBossが設定されていた場合
			else if (cumulativeTime >= _stageData.TimeTillBoss && !spawner.IsBossInited) {
				//時間が経過していればボスをInit
				StartCoroutine (InitBoss (spawner));
				spawner.IsBossInited = true;
				//雑魚敵の排出を阻止。
				spawner.spawn = false;
			}
		}

		cumulativeTime += Time.deltaTime;
	}

	private IEnumerator InitBoss(Spawner spawner) {
		yield return new WaitForSeconds (5.0f);

		EnemyData bossEnemyData = ParseEnemyData (_stageData.SpawnBoss);
		GameObject bossEnemyObj = (GameObject)Instantiate (Resources.Load ("Prefabs/Enemies/" + bossEnemyData.Prefab));

		int id = GameManager.Instance.crntEnemyDictionary.Count + 1;
		bossEnemyObj.GetComponent<EnemyObject> ().EnemyData = bossEnemyData;
		bossEnemyObj.GetComponent<EnemyObject> ().IsBoss = true;

		bossEnemyObj.transform.position = spawner.transform.position;
	}

	/**
	 * 
	 * Stage.csvの、Spawn#を元に、
	 * Spawner#のデータ用EnemyDataを作成。
	 * 
	 */
	private EnemyData ParseEnemyData(string enemyDataStr) {
		if (string.IsNullOrEmpty (enemyDataStr)) {
			return null;
		}

		string[] stage1Splited = enemyDataStr.Split ('|');

		EnemyData enemy = new EnemyData ();
		//Enemy Prefab
		enemy.Prefab = stage1Splited [0];
		//HP
		string hpStr = stage1Splited [1].Split(':')[1];
		enemy.HP = Int32.Parse (hpStr);
		//Speed
		string speedStr = stage1Splited [2].Split(':')[1];
		enemy.Speed = float.Parse (speedStr);

		return enemy;
	}




	/**
	 * 
	 * Stage.csvの、Dropsを元に、
	 * ドロップするアイテムを生成。
	 * 
	 * 敵を倒した際、%でアイテムドロップ。
	 * ステージクリア時に最低1つプレゼント。
	 * 
	 */
	private void ParseDropData(string dropDataStr) {
		if (string.IsNullOrEmpty (dropDataStr)) {
			return ;
		}

		string[] dropSplited = dropDataStr.Split ('|');

		foreach(string dropStr in dropSplited) {
			DropData dropData = new DropData ();
			dropData.ID = dropStr.Split(':')[0];
			dropData.Percentage = float.Parse(dropStr.Split(':')[1]);

			dropDataList.Add (dropData);
		}
	}

	/**
	 * 
	 * EnemyをInitする毎にそのEnemyが倒された際に
	 * ドロップするかを確認。
	 * 
	 */
	public DropData MakeDrop() {
		int MAX_NUM = 10000;
		foreach (DropData possibleDrop in dropDataList) {
			float percent = possibleDrop.Percentage*MAX_NUM;
			int rand = UnityEngine.Random.Range(0, MAX_NUM);

			if (rand <= (int)(percent)) {
				//drop
				return possibleDrop;
			}
		}
		return null;
	}
}
