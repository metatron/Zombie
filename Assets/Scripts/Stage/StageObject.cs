using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour {
	private StageData _stageData;

	public List<Spawner> spawnerList = new List<Spawner>();


	public void InitStageObject(StageData stageData) {
		_stageData = stageData;
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
		//Spawnerが終ってるかどうか定期的に確認
		foreach (Spawner spawner in spawnerList) {
			//終ってたらbossをInit
			if (spawner.IsFinishedSpawning () && !spawner.IsBossInited) {
				StartCoroutine(InitBoss());
				spawner.IsBossInited = true;
			}
		}
	}

	private IEnumerator InitBoss() {
		yield return new WaitForSeconds (5.0f);
		Debug.LogError ("**************InitBoss: " +_stageData.SpawnBoss);
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
}
